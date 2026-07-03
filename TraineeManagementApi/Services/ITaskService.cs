namespace TraineeManagementApi.Services;

using TraineeManagementApi.DTO;

public interface ILearningTaskService
{

    Task<Result<SearchResponse>> GetAll(SearchQuery searchQuery);
    Task<Result<LearningTaskResponse>> GetById(long id);
    Task<Result<long>> PostById( CreateLearningTaskRequest task);
    Task<Result<long>> PutById(UpdateLearningTaskRequest task);

    Task<Result<long>> DeleteById(long id);
}