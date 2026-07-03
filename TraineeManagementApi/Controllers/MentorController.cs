namespace TraineeManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TraineeManagementApi.DTO;
using TraineeManagementApi.Services;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class MentorController : ControllerBase
{
    private readonly IMentorService _mentorService;

    public MentorController(IMentorService mentorService)
    {
        _mentorService = mentorService;
    }
    
    [HttpGet(Name = "GetAllMentors")]
    public async Task<IActionResult> GetAll([FromQuery] SearchQuery searchQuery)
    {
        var response = await _mentorService.GetAll(searchQuery);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        return Ok(response.Value);
    }
        
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        var response = await _mentorService.GetById(id);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        return Ok(response.Value);
    }
        
    [HttpPost()]
    public async Task<IActionResult> PostById(CreateMentorRequest mentor)
    {
        var response = await _mentorService.PostById(mentor);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        return Created();
    }

    [HttpPut()]
    public async Task<IActionResult> PutById(UpdateMentorRequest mentor)
    {
        var response = await _mentorService.PutById(mentor);
        if (!response.IsSuccess)
        {
            if (string.Equals(response.Error, "Mentor not found.", StringComparison.OrdinalIgnoreCase))
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
        var response = await _mentorService.DeleteById(id);
        if (!response.IsSuccess)
        {
            return NotFound();
        }
        return NoContent();
    }
}


