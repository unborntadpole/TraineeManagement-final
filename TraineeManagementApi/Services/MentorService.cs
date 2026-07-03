namespace TraineeManagementApi.Services;

using TraineeManagementApi.DTO;

using TraineeManagementApi.Models;

using TraineeManagementApi.db;


public class MentorService : IMentorService
{
    private readonly IMentorRepository _repository;
    private readonly ILogger<MentorService> _logger;

    public MentorService(IMentorRepository repository, ILogger<MentorService> logger)
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task<Result<SearchResponse>> GetAll(SearchQuery searchQuery)
    {
        var mentors = await _repository.GetAllAsync(searchQuery.search, searchQuery.status, searchQuery.pageNumber, searchQuery.pageSize);
        if (mentors == null)
        {
            _logger.LogWarning("Mentor: Get All failed: No mentors found with given keywords");
            return Result<SearchResponse>.Failure("");
        }
        SearchResponse response = new SearchResponse(mentors, searchQuery.pageNumber, searchQuery.pageSize);
        _logger.LogInformation("Mentor: Get All LogInformation");
        return Result<SearchResponse>.Success(response);
    }

    public async Task<Result<MentorResponse>> GetById(long id)
    {
        var mentor = await _repository.GetByIdAsync(id);
        if (mentor == null)
        {
            _logger.LogWarning("Mentor: Get with Id failed: No mentor found with given Id");
            return Result<MentorResponse>.Failure("Mentor not found.");
        }
        MentorResponse response = new MentorResponse(mentor);
        _logger.LogInformation("Mentor: Get with Id successful");
        return Result<MentorResponse>.Success(response);
    }

    public async Task<Result<long>> PostById( CreateMentorRequest mentor)
    {
        Mentor mentor1 = new Mentor(mentor);
        await _repository.AddAsync(mentor1);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Mentor: Post successful");
        return Result<long>.Success(200);
    }

    public async Task<Result<long>> PutById(UpdateMentorRequest mentor)
    {
        var old_mentor = await _repository.GetByIdAsync(mentor.Id);
        if (old_mentor == null)
        {
            _logger.LogWarning("Mentor: Put by Id failed - Id not found");
            return Result<long>.Failure("Mentor not found.");
        }
        try
        {
            old_mentor.FirstName = mentor.FirstName;
            old_mentor.LastName = mentor.LastName;
            old_mentor.Email = mentor.Email;
            old_mentor.Expertise = mentor.Expertise;
            old_mentor.Status = mentor.Status;
            old_mentor.UpdatedDate = DateTime.UtcNow;
            _repository.Update(old_mentor);
            await _repository.SaveChangesAsync();
            _logger.LogInformation("Mentor: Put by Id successful");
            return Result<long>.Success(200);
            
        }
        catch
        {
            _logger.LogWarning("Mentor: Put by Id failed - Invalid Data");
            return Result<long>.Failure("Invalid data");
        }
    }


    public async Task<Result<long>> DeleteById(long id)
    {
        var mentor = await _repository.GetByIdAsync(id);
        if (mentor == null)
        {
            _logger.LogWarning("Mentor: Delete by Id failed - Id not found");
            return Result<long>.Failure("Id not found");
        }
        _repository.Delete(mentor);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Mentor: Delete by Id successful");
        return Result<long>.Success(200);
    }

}
