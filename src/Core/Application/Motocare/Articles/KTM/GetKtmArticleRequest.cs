namespace FSH.WebApi.Application.Motocare.Articles.Ktm;

public class GetKtmArticleRequest : IRequest<KtmArticleDto>
{
    public string ArticleNumber { get; set; }

    public GetKtmArticleRequest(string articleNumber) => ArticleNumber = articleNumber;
}

public class KtmArticleByIdSpec : Specification<Domain.Motocare.Articles.KtmArticle, KtmArticleDto>, ISingleResultSpecification
{
    public KtmArticleByIdSpec(string articleNumber) => Query.Where(x => x.ArticleNumber == articleNumber);
}

public class GetKtmArticleRequestHandler : IRequestHandler<GetKtmArticleRequest, KtmArticleDto>
{
    readonly IRepository<Domain.Motocare.Articles.KtmArticle> _repository;
    readonly IStringLocalizer<GetKtmArticleRequest> _localizer;

    public GetKtmArticleRequestHandler(IRepository<Domain.Motocare.Articles.KtmArticle> repository, IStringLocalizer<GetKtmArticleRequest> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task<KtmArticleDto> Handle(GetKtmArticleRequest request, CancellationToken cancellationToken)
    {
        return await _repository.FirstOrDefaultAsync((ISpecification<Domain.Motocare.Articles.KtmArticle, KtmArticleDto>) new KtmArticleByIdSpec(request.ArticleNumber), cancellationToken) ??
               throw new NotFoundException(_localizer["KTM Article {0} Not Found.", request.ArticleNumber]);
    }
}