using System.Globalization;
using CsvHelper;
using FSH.WebApi.Domain.Motocare.Articles;

namespace FSH.WebApi.Application.Motocare.Articles.Ktm;

public class ProcessFileRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public ProcessFileRequest(Guid id) => Id = id;
}

public class ProcessFileRequestHandler : IRequestHandler<ProcessFileRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Domain.Motocare.Articles.File> _fileRepo;
    private readonly IRepositoryWithEvents<KtmArticle> _articleRepo;
    private readonly IStringLocalizer _t;

    public ProcessFileRequestHandler(IRepositoryWithEvents<Domain.Motocare.Articles.File> fileRepo, IRepositoryWithEvents<KtmArticle> articleRepo, IStringLocalizer<ProcessFileRequestHandler> localizer) =>
        (_fileRepo, _articleRepo, _t) = (fileRepo, articleRepo, localizer);

    public async Task<Guid> Handle(ProcessFileRequest request, CancellationToken cancellationToken)
    {
       var file = await _fileRepo.GetByIdAsync(request.Id, cancellationToken);

       _ = file ?? throw new NotFoundException(_t["File {0} Not Found."]);

       var articles = await _articleRepo.ListAsync(cancellationToken);

       var stream = new MemoryStream(file.Data);
       using (var reader = new StreamReader(stream))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<dynamic>();

            foreach (dynamic? record in records)
            {
                var article = articles.SingleOrDefault(x => x.ArticleNumber == record["#Article Number"]);

                if (article == null)
                {
                    article = new KtmArticle(
                        file.Name
                        , record["ArticleNumber"]
                        , record["Article Description"]
                        , record["Currency"]
                        , decimal.Parse(record["Purchase Price (excl. VAT)"])
                        , decimal.Parse(record["Retail Price (excl. VAT)"])
                        , record["Discount Group"]
                        , record["Unit of Measure"]
                        , int.Parse(record["Sales Price Quantity"])
                        , int.Parse(record["Package Size"])
                        , record["Article Status"] ?? "-99"
                        , record["Product Hierarchy"]
                        , record["Hierarchy Name1"]
                        , record["Hierarchy Name2"]
                        , record["Hierarchy Name3"]
                        , record["Hierarchy Name4"]
                        , record["Hierarchy Name5"]
                        , record["Hierarchy Name6"]
                        , record["Hierarchy Name7"]
                        , DateTime.ParseExact(record["Price valid from"], "d/MM/yyyy", CultureInfo.InvariantCulture)
                        , record["GTIN"]
                        , record["Country of Origin"]
                        , record["Imp. Code"]);

                    await _articleRepo.AddAsync(article, cancellationToken);
                }
                else
                {
                    article.Update(
                        file.Name
                        , record["ArticleNumber"]
                        , record["Article Description"]
                        , record["Currency"]
                        , decimal.Parse(record["Purchase Price (excl. VAT)"])
                        , decimal.Parse(record["Retail Price (excl. VAT)"])
                        , record["Discount Group"]
                        , record["Unit of Measure"]
                        , int.Parse(record["Sales Price Quantity"])
                        , int.Parse(record["Package Size"])
                        , record["Article Status"] ?? "-99"
                        , record["Product Hierarchy"]
                        , record["Hierarchy Name1"]
                        , record["Hierarchy Name2"]
                        , record["Hierarchy Name3"]
                        , record["Hierarchy Name4"]
                        , record["Hierarchy Name5"]
                        , record["Hierarchy Name6"]
                        , record["Hierarchy Name7"]
                        , !string.IsNullOrWhiteSpace(record["Price valid from"])
                            ? DateTime.ParseExact(record["Price valid from"], "d/MM/yyyy", CultureInfo.InvariantCulture)
                            : article.PriceValidFrom
                        , record["GTIN"]
                        , record["Country of Origin"]
                        , record["Imp. Code"]);

                    await _articleRepo.UpdateAsync(article, cancellationToken);
                }

            }

        }

       file.Update(DateTime.Now);
       await _fileRepo.UpdateAsync(file, cancellationToken);

       return request.Id;
    }
}