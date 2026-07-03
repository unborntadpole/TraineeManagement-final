namespace TraineeManagementApi.Services;

using TraineeManagementApi.DTO;

public interface IMentorService
{

    Task<Result<SearchResponse>> GetAll(SearchQuery searchQuery);
    Task<Result<MentorResponse>> GetById(long id);
    Task<Result<long>> PostById( CreateMentorRequest mentor);
    Task<Result<long>> PutById(UpdateMentorRequest mentor);

    Task<Result<long>> DeleteById(long id);
}