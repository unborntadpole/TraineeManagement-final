using TraineeManagementApi.DTO;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace TraineeManagementApi.Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    [AllowedValues(["Admin","Mentor","Trainee"], ErrorMessage = "Enter a valid role")]
    public string Role { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
}
