namespace TraineeManagementApi.Services;

using TraineeManagementApi.DTO;

public interface ITraineeService
{

    Task<Result<SearchResponse>> GetAll(SearchQuery searchQuery);
    Task<Result<TraineeResponse>> GetById(long id);
    Task<Result<string>> PostById( TraineeRequest trainee);
    Task<Result<string>> PutById(TraineeRequest trainee);

    Task<Result<string>> DeleteById(long id);
}