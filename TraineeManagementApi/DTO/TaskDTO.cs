namespace TraineeManagementApi.DTO;
using TraineeManagementApi.Models;
using System.ComponentModel.DataAnnotations;

public class CreateLearningTaskRequest
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Task title is required.")]
    [StringLength(50, ErrorMessage = "Title must be smaller than 50 characters")]
    public string Title { get; set; } = null!;
    
    [Required(ErrorMessage = "Task descrption is required.")]
    [StringLength(50, ErrorMessage = "Description must be smaller than 50 characters")]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = "ExpectedTechStack is required.")]
    [StringLength(50, ErrorMessage = "Last name must be smaller than 50 characters")]
    public string ExpectedTechStack { get; set; } = null!;

    [Required(ErrorMessage = "Due date is required.")]
    public DateTime DueDate { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [AllowedValues(["Draft", "Published", "Closed"], ErrorMessage = "Enter a valid status - Draft, Published or Closed")]
    public string Status { get; set; } = null!;
}

public class UpdateLearningTaskRequest
{
    [Required(ErrorMessage = "ID is required.")]
    public long Id { get; set; }

    [Required(ErrorMessage = "Task title is required.")]
    [StringLength(50, ErrorMessage = "Title must be smaller than 50 characters")]
    public string Title { get; set; } = null!;
    
    [Required(ErrorMessage = "Task descrption is required.")]
    [StringLength(50, ErrorMessage = "Description must be smaller than 50 characters")]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = "ExpectedTechStack is required.")]
    [StringLength(50, ErrorMessage = "Last name must be smaller than 50 characters")]
    public string ExpectedTechStack { get; set; } = null!;

    [Required(ErrorMessage = "Due date is required.")]
    public DateTime DueDate { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [AllowedValues(["Draft", "Published", "Closed"], ErrorMessage = "Enter a valid status")]
    public string Status { get; set; } = null!;
    public DateTime UpdatedDate { get; } = DateTime.UtcNow;
}

public class LearningTaskResponse
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ExpectedTechStack { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedDate { get; set;}
    public DateTime UpdatedDate { get; set; }

    // public LearningTaskResponse() {}

    public LearningTaskResponse(LearningTask task)
    {
        Id = task.Id;
        Title = task.Title;
        Description = task.Description;
        ExpectedTechStack = task.ExpectedTechStack;
        DueDate = task.DueDate;
        Status = task.Status;
        CreatedDate = task.CreatedDate;
        UpdatedDate = task.UpdatedDate;
    }
}
