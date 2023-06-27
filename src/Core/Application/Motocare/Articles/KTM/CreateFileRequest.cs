using System.Security.AccessControl;
using System.Security.Cryptography;

namespace FSH.WebApi.Application.Motocare.Articles.Ktm;

public class CreateFileRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public byte[] Data { get; set; }
}

public class CreateFileRequestValidator : CustomValidator<CreateFileRequest>
{
    public CreateFileRequestValidator(IReadRepository<Domain.Motocare.Articles.File> repository,
        IStringLocalizer<CreateFileRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75);
}

public class CreateFileRequestHandler : IRequestHandler<CreateFileRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Domain.Motocare.Articles.File> _repository;

    public CreateFileRequestHandler(IRepositoryWithEvents<Domain.Motocare.Articles.File> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateFileRequest request, CancellationToken cancellationToken)
    {
        var file = new Domain.Motocare.Articles.File(request.Name, request.Data);

        await _repository.AddAsync(file, cancellationToken);

        return file.Id;
    }
}