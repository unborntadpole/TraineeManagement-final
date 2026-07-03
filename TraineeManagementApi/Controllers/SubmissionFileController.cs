namespace TraineeManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TraineeManagementApi.Services;


[Authorize]
[ApiController]
[Route("/api/submission-files")]
public class SubmissionFileController: ControllerBase
{
    private readonly SubmissionFileService _service;

    public SubmissionFileController(SubmissionFileService service)
    {
        _service = service;
    }

    [HttpGet("{Id:long}/download")]
    public async Task<IActionResult> Download(long Id)
    {
        var res = await _service.GetFile(Id);
        if (!res.IsSuccess)
        {
            return StatusCode(res.ErrorCode, res.Error);
        } 
        return File(res.Value.Stream, res.Value.ContentType, res.Value.FileName);
    }

    [HttpDelete("{Id:long}")]
    public async Task<IActionResult> Delete(long Id)
    {
        var res = await _service.DeleteFile(Id);
        if (!res.IsSuccess)
        {
            return StatusCode(res.ErrorCode, res.Error);
        } 
        return StatusCode(204);
    }
}