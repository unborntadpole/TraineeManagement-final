namespace TraineeManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TraineeManagementApi.DTO;
using TraineeManagementApi.Services;
using TraineeManagementApi.Constants;


[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class TaskAssignmentController : ControllerBase
{
    private readonly TaskAssignmentService _service;
    
    // private enum ValidStatus
    // {
    //     Assigned,
    //     InProgress,
    //     Submitted,
    //     Reviewed,
    //     Completed 
    // } 

    public TaskAssignmentController(TaskAssignmentService service)
    {
        _service = service;
    }
    
    [HttpGet(Name = "GetAllTaskAssignments")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _service.GetAll();
        if (!response.IsSuccess)
        {
            if (response.ErrorCode == 500)
            {
                return StatusCode(response.ErrorCode, response.Error);
            }
            else return NotFound(response.Error);
        }
        return Ok(response.Value);
    }
        
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var response = await _service.GetById(id);
        if (!response.IsSuccess)
        {
            if (response.ErrorCode == 500)
            {
                return StatusCode(response.ErrorCode, response.Error);
            }
            else return NotFound();
        }
        return Ok(response.Value);
    }
        
    [HttpPost()]
    public async Task<IActionResult> PostById(TaskAssignmentDTO taskAssignmentDTO)
    {
        // var validator = _createRequestValidator.Validate(taskAssignmentDTO);
        // if (! validator.IsValid)
        // {
        //     return BadRequest(validator.Errors);
        // }
        if (! Enum.IsDefined(typeof(StatusEnums.TaskAssignmentStatus),taskAssignmentDTO.Status))
        {
            return BadRequest("Invalid status");
        }
        var response = await _service.PostById(taskAssignmentDTO);
        if (!response.IsSuccess)
        {
            if (response.ErrorCode == 500)
            {
                return StatusCode(response.ErrorCode, response.Error);
            }
            else return NotFound(response.Error);
        }
        // return Ok();
        // return StatusCode(StatusCodes.Status201Created, trainee);
        return Created();
    }

    [HttpPut("{id:long}/status")]
    public async Task<IActionResult> PutById([FromRoute] long id, [FromBody] string status)
    {
        
        if (! Enum.IsDefined(typeof(StatusEnums.TaskAssignmentStatus),status))
        {
            return BadRequest("Invalid status");
        }
        var response = await _service.PutById(status, id);
        if (!response.IsSuccess)
        {

            if (response.ErrorCode == 500)
            {
                return StatusCode(response.ErrorCode, response.Error);
            }
            else return NotFound(response.Error);
        }
        return Ok();
    }
}


