using FSH.WebApi.Application.Motocare.Articles.Ktm;

namespace FSH.WebApi.Host.Controllers.Motocare;

public class ArticlesController : VersionedApiController
{
    [HttpGet("{articleNumber}")]
    [MustHavePermission(FSHAction.View, FSHResource.Motocare)]
    [OpenApiOperation("Get article details.", "")]
    public async Task<KtmArticleDto> GetAsync(string articleNumber)
    {
        return await Mediator.Send(new GetKtmArticleRequest(articleNumber));
    }

    [HttpPost("UploadArticleFile")]
    [MustHavePermission(FSHAction.View, FSHResource.Motocare)]
    [OpenApiOperation("Upload KTM Article file for.", "")]
    public async Task<ActionResult> Upload([FromForm] CreateFileRequest request)
    {
        var result = await Mediator.Send(request);

        return Ok(result);
    }
}