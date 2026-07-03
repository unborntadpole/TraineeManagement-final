namespace TraineeManagementApi.Models;
using TraineeManagementApi.DTO;

public class TaskAssignment
{
    public long Id { get; set; }
    public string Status { get; set; }
    public DateTime AssignedDate { get; set; }
    public DateTime DueDate { get; set; }
    public string? Remarks { get; set; }

    public long TraineeId { get; set; }
    public Trainee Trainee { get; set; }

    public long MentorId { get; set; }
    public Mentor Mentor { get; set; }

    public long LearningTaskId { get; set; }
    public LearningTask LearningTask { get; set; }

    // public long SubmissionId {a get; set; }
    // public Submission? Submission { get; set; }

    public ICollection<Submission> Submissions { get; }

    public TaskAssignment(TaskAssignmentDTO taskAssignment)
    {
        Id = taskAssignment.Id;
        AssignedDate = taskAssignment.AssignedDate;
        DueDate = taskAssignment.DueDate;
        Remarks = taskAssignment.Remarks;
        TraineeId = taskAssignment.TraineeId;
        MentorId = taskAssignment.MentorId;
        LearningTaskId = taskAssignment.LearningTaskId;
        // SubmissionId = taskAssignment.SubmissionId;
        Status = taskAssignment.Status;
    }
    public TaskAssignment() {}

}