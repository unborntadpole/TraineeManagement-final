namespace TraineeManagementApi.Models;
using TraineeManagementApi.DTO;

public class Review
{
    public long Id { get; set; }
    public string ReviewStatus { get; set; }
    public string Feedback { get; set; }
    public int Score { get; set; }
    public DateTime ReviewedDate { get; set; }

    public long MentorId { get; set; }
    public Mentor Mentor { get; set; }

    public long SubmissionId { get; set; }
    public Submission Submission { get; set; }



    public Review(ReviewDTO review)
    {
        Id = review.Id;
        MentorId = review.MentorId;
        SubmissionId = review.SubmissionId;
        ReviewedDate = DateTime.UtcNow;
        Feedback = review.Feedback;
        Score = review.Score;
        ReviewStatus = review.ReviewStatus;
    }
    public Review() {}

}