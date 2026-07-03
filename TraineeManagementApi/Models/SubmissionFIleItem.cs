using TraineeManagementApi.DTO;


namespace TraineeManagementApi.Models;

public class SubmissionFile
{
    public long Id { get; set; }
    public string OriginalFileName { get; set; }
    public string GeneratedStorageName { get; set; }
    public string ContentType { get; set; }
    public long Size { get; set; }
    public string Checksum { get; set; }
    public string UploadedByUser { get; set; }
    public DateTime UploadedDate { get; set; }

    public long SubmissionId { get; set; }
    public Submission Submission{ get; set; }

    public SubmissionFile(SubmissionFileDTO submissionFile)
    {
        OriginalFileName = submissionFile.OriginalFileName;
        GeneratedStorageName = submissionFile.GeneratedStorageName;
        ContentType = submissionFile.ContentType;
        Size = submissionFile.Size;
        Checksum = submissionFile.Checksum;
        UploadedByUser = submissionFile.UploadedByUser;
        UploadedDate = DateTime.UtcNow;
        SubmissionId = submissionFile.SubmissionId;
    }
    public SubmissionFile() {}

}