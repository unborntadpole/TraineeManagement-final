using Microsoft.AspNetCore.Mvc;

namespace TraineeManagementApi.Controllers;

using TraineeManagementApi.DTO;
using TraineeManagementApi.Services;

[ApiController]
[Route("/api/auth/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public LoginController(IAuthenticationService authService)
    {
        _authService = authService;
    }
    
 
    [HttpPost()]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _authService.Login(request);
        if (!response.IsSuccess)
        {
            switch (response.Error)
            {
                case "User not found":
                return NotFound();
                case "Incorrect password":
                return Unauthorized();
            }
        }
        
        return Ok(response.Value);
    }

    [HttpPost("/admin")]
    public async Task<IActionResult> Login()
    {
        LoginRequest request = new LoginRequest()
        {
            Username = "admin",
            Password = "Admin@123"
        };
        var response = await _authService.Login(request);
        if (!response.IsSuccess)
        {
            switch (response.Error)
            {
                case "User not found":
                return NotFound();
                case "Incorrect password":
                return Unauthorized();
            }
        }
        
        return Ok(response.Value);
    }

}


