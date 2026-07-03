namespace TraineeManagementApi.Constants;

public class StatusEnums
{
    public enum TaskAssignmentStatus
    {
        Assigned,
        InProgress,
        Submitted,
        Reviewed,
        Completed 
    }

    public enum SubmissionStatus
    {
        Submitted,
        Resubmitted
    }

    public enum ReviewStatus
    {
        Accepted,
        ChangesRequired,
        Rejected
    }

    public enum TraineeStatus
    {
        Active,
        Inactive,
        Completed
    }

    public enum ProcessingJobStatus
    {
        Queued,
        Processing,
        Completed,
        Failed
    }

}