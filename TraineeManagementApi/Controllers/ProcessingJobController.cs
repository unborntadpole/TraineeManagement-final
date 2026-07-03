namespace TraineeManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TraineeManagementApi.db;

[Authorize]
[ApiController]
[Route("/api/processing-jobs")]
public class ProcessingJobController: ControllerBase
{
    private readonly ProcessingJobsRepository _service;

    public ProcessingJobController(ProcessingJobsRepository service)
    {
        _service = service;
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(string Id)
    {
        try
        {
            var res = await _service.GetByIdAsync(Id);
            if (res == null)
            {
                return StatusCode(404, "File not found.");
            } 
            return Ok(res);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}