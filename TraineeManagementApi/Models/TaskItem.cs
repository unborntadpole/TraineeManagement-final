using TraineeManagementApi.DTO;

namespace TraineeManagementApi.Models;

public class LearningTask
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ExpectedTechStack { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    public ICollection<TaskAssignment> TaskAssignments { get; }

    public LearningTask(CreateLearningTaskRequest learningTask)
    {
        Title = learningTask.Title;
        Description = learningTask.Description;
        ExpectedTechStack = learningTask.ExpectedTechStack;
        DueDate = learningTask.DueDate;
        Status = learningTask.Status;
    }
    public LearningTask() {}

}