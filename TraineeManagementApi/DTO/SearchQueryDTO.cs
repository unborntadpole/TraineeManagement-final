namespace TraineeManagementApi.DTO;

using System.ComponentModel.DataAnnotations;

public class SearchQuery
{
    [Range(1, int.MaxValue, ErrorMessage = "Minimum pagenumber is 1 and it should be and integer")]
    public int pageNumber { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Minimum pageSize is 1 and it should be and integer")]
    public int pageSize { get; set; } = 10;

    public string? search { get; set; } = null;

    public string? status { get; set; } = null;

}