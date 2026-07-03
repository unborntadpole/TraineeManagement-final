namespace TraineeManagementApi.Models;
using TraineeManagementApi.DTO;

public class Submission
{
    public long Id { get; set; }
    public string Status { get; set; }
    public string SubmissionUrl { get; set; }
    public string Notes { get; set; }
    public DateTime SubmittedDate { get; set; }

    public long TaskAssignmentId { get; set; }
    public TaskAssignment TaskAssignment { get; set; }
    // public long ReviewId { get; set; }
    // public Review? Review { get; set; }

    public SubmissionFile? SubmissionFile { get; set; }
    public ICollection<Review> Reviews { get; }


    public Submission(SubmissionDTO submission)
    {
        Id = submission.Id;
        TaskAssignmentId = submission.TaskAssignmentId;
        // ReviewId = submission.ReviewId;
        SubmittedDate = DateTime.UtcNow;
        Notes = submission.Notes;
        SubmissionUrl = submission.SubmissionUrl;
        Status = submission.Status;
    }
    public Submission() {}

}