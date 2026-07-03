namespace TraineeManagementApi.DTO;

using System.ComponentModel.DataAnnotations;

public class LoginRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

}

public class UserResponse
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
}

public class LoginResponse
{
    public string Token { get; set; }
    public int ExpiresIn { get; set; }
    public UserResponse User { get; set; }
}

