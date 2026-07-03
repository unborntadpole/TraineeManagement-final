namespace TraineeManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TraineeManagementApi.DTO;
using TraineeManagementApi.Services;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class LearningTaskController : ControllerBase
{
    private readonly ILearningTaskService _taskService;

    public LearningTaskController(ILearningTaskService taskService)
    {
        _taskService = taskService;
    }
    
    [HttpGet(Name = "GetAllTasks")]
    public async Task<IActionResult> GetAll([FromQuery] SearchQuery searchQuery)
    {
        var response = await _taskService.GetAll(searchQuery);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        return Ok(response.Value);
    }
        
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var response = await _taskService.GetById(id);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        return Ok(response.Value);
    }
        
    [HttpPost()]
    public async Task<IActionResult> PostById(CreateLearningTaskRequest task)
    {
        var response = await _taskService.PostById(task);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        return Created();
    }

    [HttpPut()]
    public async Task<IActionResult> PutById(UpdateLearningTaskRequest task)
    {
        var response = await _taskService.PutById(task);
        if (!response.IsSuccess)
        {
            if (string.Equals(response.Error, "Task not found.", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }
        return Ok();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteById([FromRoute] long id)
    {
        var response = await _taskService.DeleteById(id);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        return NoContent();
    }
}


