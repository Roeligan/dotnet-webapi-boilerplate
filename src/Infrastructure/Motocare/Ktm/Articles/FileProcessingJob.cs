using Ardalis.Specification;
using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Application.Common.Persistence;
using FSH.WebApi.Application.Motocare.Articles.Ktm;
using FSH.WebApi.Shared.Notifications;
using Hangfire;
using Hangfire.Console.Extensions;
using Hangfire.Console.Progress;
using Hangfire.Server;
using MediatR;
using Microsoft.Extensions.Logging;
using File = FSH.WebApi.Domain.Motocare.Articles.File;

namespace FSH.WebApi.Infrastructure.Motocare.Ktm.Articles;

public class FileProcessingJob : IFileProcessingJob
{
    private readonly ILogger<FileProcessingJob> _logger;
    private readonly ISender _mediator;
    private readonly IReadRepository<File> _repository;
    private readonly IProgressBarFactory _progressBar;
    private readonly PerformingContext _performingContext;
    private readonly INotificationSender _notifications;
    private readonly ICurrentUser _currentUser;
    private readonly IProgressBar _progress;

    public FileProcessingJob(
        ILogger<FileProcessingJob> logger,
        ISender mediator,
        IReadRepository<File> repository,
        IProgressBarFactory progressBar,
        PerformingContext performingContext,
        INotificationSender notifications,
        ICurrentUser currentUser)
    {
        _logger = logger;
        _mediator = mediator;
        _repository = repository;
        _progressBar = progressBar;
        _performingContext = performingContext;
        _notifications = notifications;
        _currentUser = currentUser;
        _progress = _progressBar.Create();
    }

    private async Task NotifyAsync(string message, int progress, CancellationToken cancellationToken)
    {
        _progress.SetValue(progress);
        await _notifications.SendToUserAsync(
            new JobNotification()
            {
                JobId = _performingContext.BackgroundJob.Id,
                Message = message,
                Progress = progress
            },
            _currentUser.GetUserId().ToString(),
            cancellationToken);
    }

    [Queue("notdefault")]
    public async Task ProcessFilesAsync(CancellationToken cancellationToken)
    {
        await NotifyAsync("Your file processing has started", 0, cancellationToken);

        _logger.LogInformation("Initializing Job with Id: {jobId}", _performingContext.BackgroundJob.Id);

        var items = await _repository.ListAsync(new NonProcessedFileSpec(), cancellationToken);

        _logger.LogInformation("Files to process: {filesCount} ", items.Count.ToString());

        foreach (var item in items)
        {
            await _mediator.Send(new ProcessFileRequest(item.Id), cancellationToken);
        }

        _logger.LogInformation("All non processed files are now processed.");
    }

    [Queue("notdefault")]
    public async Task ReProcessFilesAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        await NotifyAsync("Your file processing has started", 0, cancellationToken);

        _logger.LogInformation("Initializing Job with Id: {jobId}", _performingContext.BackgroundJob.Id);

        var items = await _repository.ListAsync(new ProcessedFilesByIdSpec(ids), cancellationToken);

        _logger.LogInformation("Files to process: {filesCount} ", items.Count.ToString());

        foreach (var item in items)
        {
            await _mediator.Send(new ProcessFileRequest(item.Id), cancellationToken);
        }

        _logger.LogInformation("All non processed files are now processed.");
    }
}

public class NonProcessedFileSpec : Specification<File>
{
    public NonProcessedFileSpec() => Query.Where(x => x.ProcessedDT == null);
}

public class ProcessedFilesByIdSpec : Specification<File>
{
    public ProcessedFilesByIdSpec(IReadOnlyCollection<Guid> ids) => Query.Where(x => ids.Contains(x.Id) );
}