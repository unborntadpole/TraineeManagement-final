namespace TraineeManagementApi.DTO;
using TraineeManagementApi.Models;
using System.ComponentModel.DataAnnotations;



public class ReviewDTO
{

    [Required(ErrorMessage = "Id is required.")]
    public long Id { get; set; }

    [Required(ErrorMessage = "Submission Id is required.")]
    public long SubmissionId { get; set; }

    [Required(ErrorMessage = "Mentor Id is required.")]
    public long MentorId { get; set; }
    
    [Required(ErrorMessage = "Mentor feedback is required.")]
    public string Feedback { get; set; }

    public DateTime ReviewedDate { get; set; }
    
    public int Score { get; set; }
    
    [Required(ErrorMessage = "Status is required.")]
    public string ReviewStatus { get; set; }

    public ReviewDTO(Review review)
    {
        Id = review.Id;
        MentorId = review.MentorId;
        SubmissionId = review.SubmissionId;
        ReviewedDate = review.ReviewedDate;
        Feedback = review.Feedback;
        Score = review.Score;
        ReviewStatus = review.ReviewStatus;
    }
    public ReviewDTO() {}
}


