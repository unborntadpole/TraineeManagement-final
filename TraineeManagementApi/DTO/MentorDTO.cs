namespace TraineeManagementApi.DTO;
using TraineeManagementApi.Models;
using System.ComponentModel.DataAnnotations;
using TraineeManagementApi.Constants;

public class CreateMentorRequest
{
    [Required(ErrorMessage = "Id is required.")]
    public long Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name must be smaller than 100 characters")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name must be smaller than 100 characters")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Email address is required.")]
    [RegularExpression(RegexPatterns.Email, ErrorMessage = "Invalid email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Expertise is required.")]
    public string Expertise { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [AllowedValues(["Active", "Inactive", "Completed"], ErrorMessage = "Enter a valid status")]
    public string Status { get; set; }
}

public class UpdateMentorRequest
{
    [Required(ErrorMessage = "ID is required.")]
    public long Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name must be smaller than 50 characters")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name must be smaller than 50 characters")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Tech stack is required.")]
    public string Expertise { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public string Status { get; set; }

    public DateTime UpdatedDate { get; } = DateTime.UtcNow;
}

public class MentorResponse
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Expertise { get; set; }
    public string Status { get; set; }
    
    public DateTime CreatedDate { get; set;}
    public DateTime UpdatedDate { get; set; }

    public MentorResponse() {}

    public MentorResponse(Mentor mentor)
    {
        Id = mentor.Id;
        FirstName = mentor.FirstName;
        LastName = mentor.LastName;
        Email = mentor.Email;
        Expertise = mentor.Expertise;
        Status = mentor.Status;
        CreatedDate = mentor.CreatedDate;
        UpdatedDate = mentor.UpdatedDate;
    }
}

