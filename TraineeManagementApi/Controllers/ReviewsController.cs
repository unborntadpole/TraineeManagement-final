namespace TraineeManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TraineeManagementApi.DTO;
using TraineeManagementApi.Services;
using TraineeManagementApi.Constants;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly ReviewService _service;
    
    // private enum ValidStatus
    // {
    //     Accepted,
    //     ChangesRequired,
    //     Rejected
    // }

    public ReviewsController(ReviewService service)
    {
        _service = service;
    }
    
    [HttpGet(Name = "GetAllReviews")]
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
    public async Task<IActionResult> PostById(ReviewDTO reviewDTO)
    {

        if (! Enum.IsDefined(typeof(StatusEnums.ReviewStatus),reviewDTO.ReviewStatus))
        {
            return BadRequest("Invalid status");
        }
        var response = await _service.PostById(reviewDTO);
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

}


