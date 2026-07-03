namespace TraineeManagementApi.DTO;
using TraineeManagementApi.Models;
using System.ComponentModel.DataAnnotations;



public class SubmissionDTO
{

    // [Required(ErrorMessage = "Id is required.")]
    public long Id { get; set; }

    [Required(ErrorMessage = "Task assignment Id is required.")]
    public long TaskAssignmentId { get; set; }

    // public long ReviewId { get; set; }
    
    [Required(ErrorMessage = "Submission Url is required.")]
    public string SubmissionUrl { get; set; }

    public DateTime SubmittedDate { get; set; }
    
    [Required(ErrorMessage = "Notes are required.")]
    public string Notes { get; set; }
    
    [Required(ErrorMessage = "Status is required.")]
    public string Status { get; set; }

    public SubmissionDTO(Submission submission)
    {
        Id = submission.Id;
        TaskAssignmentId = submission.TaskAssignmentId;
        // ReviewId = submission.ReviewId;
        SubmittedDate = submission.SubmittedDate;
        Notes = submission.Notes;
        SubmissionUrl = submission.SubmissionUrl;
        Status = submission.Status;
    }
    public SubmissionDTO() {}
}


