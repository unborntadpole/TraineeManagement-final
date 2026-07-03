namespace TraineeManagementApi.DTO;

using System.ComponentModel.DataAnnotations;

public class SearchResponse
{
    
    public int pageNumber { get; set; }
    public int pageSize { get; set; }
    public int totalRecords { get; set; }
    public object data { get; set; }
    public SearchResponse(List<TraineeResponse> data, int pageNumber, int pageSize)
    {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;
        totalRecords = data.Count();
        this.data = data;

    }
    public SearchResponse(List<MentorResponse> data, int pageNumber, int pageSize)
    {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;
        totalRecords = data.Count();
        this.data = data;

    }

    public SearchResponse(List<LearningTaskResponse> data, int pageNumber, int pageSize)
    {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;
        totalRecords = data.Count();
        this.data = data;

    }
}