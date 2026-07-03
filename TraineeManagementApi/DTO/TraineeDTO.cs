namespace TraineeManagementApi.DTO;
using TraineeManagementApi.Models;
using System.ComponentModel.DataAnnotations;

public class TraineeRequest
{
    public long Id { get; set; }

    // [Required(ErrorMessage = "First name is required.")]
    // [StringLength(50, ErrorMessage = "First name must be smaller than 50 characters")]
    public string FirstName { get; set; } = null!;
    
    // [Required(ErrorMessage = "Last name is required.")]
    // [StringLength(50, ErrorMessage = "Last name must be smaller than 50 characters")]
    public string LastName { get; set; } = null!;
    
    // [Required(ErrorMessage = "Email address is required.")]
    // [EmailAddress(ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;

    // [Required(ErrorMessage = "Tech stack is required.")]
    public string TechStack { get; set; } = null!;

    // [Required(ErrorMessage = "Status is required.")]
    // [AllowedValues(["Active", "Inactive", "Completed"], ErrorMessage = "Enter a valid status")]
    public string Status { get; set; } = null!;
}

// public class UpdateTraineeRequest
// {
//     // [Required(ErrorMessage = "ID is required.")]
//     public long Id { get; set; }

//     // [Required(ErrorMessage = "First name is required.")]
//     // [StringLength(50, ErrorMessage = "First name must be smaller than 100 characters")]
//     public string FirstName { get; set; } = null!;
    
//     // [Required(ErrorMessage = "Last name is required.")]
//     // [StringLength(50, ErrorMessage = "Last name must be smaller than 100 characters")]
//     public string LastName { get; set; } = null!;

//     // [Required(ErrorMessage = "Email address is required.")]
//     // [EmailAddress(ErrorMessage = "Invalid email")]
//     public string Email { get; set; } = null!;

//     // [Required(ErrorMessage = "Tech stack is required.")]
//     public string TechStack { get; set; } = null!;

//     // [Required(ErrorMessage = "Status is required.")]
//     public string Status { get; set; } = null!;

// }

public class TraineeResponse
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!; 
    public string LastName { get; set; } = null!; 
    public string Email { get; set; } = null!; 
    public string TechStack { get; set; } = null!; 
    public string Status { get; set; } = null!; 
    
    public DateTime CreatedDate { get; set;}
    public DateTime UpdatedDate { get; set; }

    public TraineeResponse() {}

    public TraineeResponse(Trainee trainee)
    {
        Id = trainee.Id;
        FirstName = trainee.FirstName;
        LastName = trainee.LastName;
        Email = trainee.Email;
        TechStack = trainee.TechStack;
        Status = trainee.Status;
        CreatedDate = trainee.CreatedDate;
        UpdatedDate = trainee.UpdatedDate;
    }
}

