using System.ComponentModel;

namespace FSH.WebApi.Application.Motocare.Articles.Ktm;

public interface IFileProcessingJob : IScopedService
{
    [DisplayName("Processes non processed Article files")]
    Task ProcessFilesAsync(CancellationToken cancellationToken);

    [DisplayName("Reprocesses already processed Article files")]
    Task ReProcessFilesAsync(List<Guid> ids, CancellationToken cancellationToken);
}
