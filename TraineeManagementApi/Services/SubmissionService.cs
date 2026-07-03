namespace TraineeManagementApi.Services;

using TraineeManagementApi.DTO;
using TraineeManagementApi.Models;
using TraineeManagementApi.db;
using TraineeManagementApi.Constants;

public class SubmissionService
{
    private readonly SubmissionRepository _repository;
    private readonly ILogger<SubmissionService> _logger;

    public SubmissionService(SubmissionRepository repository, ILogger<SubmissionService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<List<SubmissionDTO>>> GetAll()
    {
        List<SubmissionDTO> submissions;
        try
        {
            submissions = await _repository.GetAllAsync();
        }
        catch
        {
            _logger.LogWarning("Submission: Get All failed: Failed to connect to database");
            return Result<List<SubmissionDTO>>.ServerError("Failed to connect to database..", 500);
        }
        if (submissions == null)
        {
            _logger.LogWarning("Submission: Get All failed: No submissions found");
            return Result<List<SubmissionDTO>>.Failure("");
        }
        _logger.LogInformation("Submission: Get All LogInformation");
        return Result<List<SubmissionDTO>>.Success(submissions);
    }
    
    public async Task<Result<SubmissionDTO>> GetById(long id)
    {
        Submission? submission;
        try{
            SubmissionDTO? redisResponse = await RedisCacheHelper.GetObjectAsync<SubmissionDTO>($"Submission:{id}", _logger);
            if(redisResponse != null)
            {
                return Result<SubmissionDTO>.SuccessWithCode(redisResponse, 200);
            }
        }
        catch(Exception e)
        {
            _logger.LogWarning($"Redis get failed.\n{e}");
        }
        try
        {
            submission = await _repository.GetByIdAsync(id);
        }
        catch
        {
            _logger.LogWarning("Submission: Get by Id failed: Failed to connect to database");
            return Result<SubmissionDTO>.ServerError("Failed to connect to database..", 500);
        }
        if (submission == null)
        {
            _logger.LogWarning("Submission: Get by Id failed: No submission found with given id");
            return Result<SubmissionDTO>.Failure("No submission with given id");
        }
        _logger.LogInformation("Submission: Get By Id executed successfully");
        SubmissionDTO submissionDTO = new SubmissionDTO(submission);
        try
        {
            await RedisCacheHelper.SetObjectAsync<SubmissionDTO>($"Submission:{id}", submissionDTO, TimeToLiveRedis.Submission, _logger);
        }
        catch
        {
            _logger.LogWarning("Redis set failed");
        }
        return Result<SubmissionDTO>.Success(submissionDTO);
    }

    public async Task<Result<long>> PostById( SubmissionDTO submissionDTO)
    {
        Submission submission = new Submission(submissionDTO);
        try
        {
            await _repository.AddAsync(submission);
            await _repository.SaveChangesAsync();
        }
        catch
        {
            _logger.LogWarning("Submission: Post failed. Database problems");
            return Result<long>.ServerError("Post failed due to database problems",500);
        }
        try
        {
            SubmissionDTO submissionDTO1 = new SubmissionDTO(submission);
            await RedisCacheHelper.SetObjectAsync<SubmissionDTO>($"Submission:{submissionDTO1.Id}", submissionDTO1, TimeToLiveRedis.Submission, _logger);
        }
        catch
        {
            _logger.LogWarning("Redis set failed");
        }
        _logger.LogInformation("Submission: Post successful");
        return Result<long>.Success(200);
    }


}