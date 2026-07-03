namespace TraineeManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TraineeManagementApi.DTO;
using TraineeManagementApi.Services;
using TraineeManagementApi.Constants;


[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class SubmissionsController : ControllerBase
{
    private readonly SubmissionService _service;
    private readonly SubmissionFileService _fileService;

    public SubmissionsController(SubmissionService service, SubmissionFileService fileService)
    {
        _service = service;
        _fileService = fileService;
    }
    
    [HttpGet(Name = "GetAllSubmissions")]
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
    public async Task<IActionResult> Post(SubmissionDTO submissionDTO)
    {
        // var validator = _createRequestValidator.Validate(taskAssignmentDTO);
        // if (! validator.IsValid)
        // {
        //     return BadRequest(validator.Errors);
        // }

        if (! Enum.IsDefined(typeof(StatusEnums.SubmissionStatus),submissionDTO.Status))
        {
            return BadRequest("Invalid status");
        }
        var response = await _service.PostById(submissionDTO);
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

    [HttpPost("{submissionId:long}/files")]
    public async Task<IActionResult> PostFile([FromForm]UploadFileRequest request, [FromRoute]long submissionId)
    {
        if (request.File == null || request.File.Length == 0)
        {
            return BadRequest("Empty file");
        }
        var response = await _fileService.PostFile(request.File, request.User, submissionId);
        if (!response.IsSuccess)
        {
            return StatusCode(response.ErrorCode, response.Error);
        }
        return StatusCode(201, response.Value);
    }
}


