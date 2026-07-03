namespace TraineeManagementApi.DTO;
using TraineeManagementApi.Models;
using System.ComponentModel.DataAnnotations;

public class TaskAssignmentDTO
{
    [Required(ErrorMessage = "Id is required.")]
    public long Id { get; set; }

    [Required(ErrorMessage = "Trainee Id is required.")]
    public long TraineeId { get; set; }
    
    [Required(ErrorMessage = "MentorId is required.")]
    public long MentorId { get; set; }
    
    [Required(ErrorMessage = "Learning task Id is required.")]
    public long LearningTaskId { get; set; }

    // public long SubmissionId { get; set; }
    
    [Required(ErrorMessage = "Assigned date is required.")]
    public DateTime AssignedDate { get; set; }
    
    [Required(ErrorMessage = "Due date is required.")]
    public DateTime DueDate { get; set; }
    
    [Required(ErrorMessage = "Status is required.")]
    public string Status { get; set; } = null!;
    public string? Remarks { get; set; }

    public TaskAssignmentDTO(TaskAssignment taskAssignment)
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

    public TaskAssignmentDTO() {}
}


