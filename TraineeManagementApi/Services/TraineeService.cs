namespace TraineeManagementApi.Services;

using TraineeManagementApi.DTO;

using TraineeManagementApi.Models;

using TraineeManagementApi.db;

using TraineeManagementApi.Constants;


public class TraineeService : ITraineeService
{
    private readonly ITraineeRepository _repository;
    private readonly ILogger<TraineeService> _logger;

    public TraineeService(ITraineeRepository repository, ILogger<TraineeService> logger)
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task<Result<SearchResponse>> GetAll(SearchQuery searchQuery)
    {
        List<TraineeResponse> trainees;
        try
        {
            trainees = await _repository.GetAllAsync(searchQuery.search, searchQuery.status, searchQuery.pageNumber, searchQuery.pageSize);
        }
        catch
        {
            _logger.LogWarning("Trainee: Get All failed: Problems with database");
            return Result<SearchResponse>.ServerError("Problems with database..", 500);
        }
        if (trainees == null)
        {
            _logger.LogWarning("Trainee: Get All failed: No trainees found with given keywords");
            // return Result<SearchResponse>.Failure("No trainees found with given keywords");
            return Result<SearchResponse>.ServerError("No trainees found with given keywords", 404);
        }
        SearchResponse response = new SearchResponse(trainees, searchQuery.pageNumber, searchQuery.pageSize);
        _logger.LogInformation("Trainee: Get All LogInformation");
        return Result<SearchResponse>.SuccessWithCode(response, 200);
    }

    public async Task<Result<TraineeResponse>> GetById(long id)
    {
        Trainee? trainee;
        try{
            TraineeResponse? redisResponse = await RedisCacheHelper.GetObjectAsync<TraineeResponse>($"Trainee:{id}", _logger);
            if(redisResponse != null)
            {
                return Result<TraineeResponse>.SuccessWithCode(redisResponse, 200);
            }
        }
        catch(Exception e)
        {
            _logger.LogWarning($"Redis get failed.\n{e}");
        }
        try
        {
            trainee = await _repository.GetByIdAsync(id);
        }
        catch
        {
            _logger.LogWarning("Trainee: Get By Id failed: Problems with database");
            return Result<TraineeResponse>.ServerError("Problems with database..", 500);
        }
        if (trainee == null)
        {
            _logger.LogWarning("Trainee: Get with Id failed: No trainee found with given Id");
            // return Result<TraineeResponse>.Failure("Trainee not found.");
            return Result<TraineeResponse>.ServerError("Trainee not found.", 404);
        }
        TraineeResponse response = new TraineeResponse(trainee);
        try
        {
            _logger.LogInformation("Trainee: Get with Id successful");
            await RedisCacheHelper.SetObjectAsync<TraineeResponse>($"Trainee:{id}", response, TimeToLiveRedis.Trainee, _logger);
        }
        catch
        {
            _logger.LogWarning("Redis set failed");
        }
        // return Result<TraineeResponse>.Success(response);
        return Result<TraineeResponse>.SuccessWithCode(response, 200);
    }

    public async Task<Result<string>> PostById( TraineeRequest trainee)
    {
        Trainee trainee1 = new Trainee(trainee);
        try
        {
            await _repository.AddAsync(trainee1);
            await _repository.SaveChangesAsync();
            _logger.LogInformation("Trainee: Post successful");

        }
        catch
        {
            _logger.LogWarning("Trainee: Create request failed: Problems with database");
            return Result<string>.ServerError("Problems with database..", 500);
        }
        // return Result<string>.Success("200");
        try
        {
            TraineeResponse response = new TraineeResponse(trainee1);
            await RedisCacheHelper.SetObjectAsync<TraineeResponse>($"Trainee:{response.Id}", response, TimeToLiveRedis.Trainee, _logger);
        }
        catch
        {
            _logger.LogWarning("Redis set failed");
        }
        return Result<string>.SuccessWithCode("Post Successful", 201);
    }

    public async Task<Result<string>> PutById(TraineeRequest trainee)
    {
        Trainee? old_trainee;
        try
        {
            old_trainee = await _repository.GetByIdAsync(trainee.Id);
        }
        catch
        {
            _logger.LogWarning("Trainee: Failed to get data");
            return Result<string>.ServerError("Problems with database..", 500);
        }
        if (old_trainee == null)
        {
            _logger.LogWarning("Trainee: Put by Id failed - Id not found");
            // return Result<string>.Failure("Trainee not found.");
            return Result<string>.ServerError("Trainee not found.", 404);
        }
        try
        {
            old_trainee.FirstName = trainee.FirstName;
            old_trainee.LastName = trainee.LastName;
            old_trainee.Email = trainee.Email;
            old_trainee.TechStack = trainee.TechStack;
            old_trainee.Status = trainee.Status;
            old_trainee.UpdatedDate = DateTime.UtcNow;
            _repository.Update(old_trainee);
            await _repository.SaveChangesAsync();
            _logger.LogInformation("Trainee: Put by Id successful");
            // return Result<string>.Success(200);
            
        }
        catch
        {
            _logger.LogWarning("Trainee: Failed to update data");
            return Result<string>.ServerError("Database error..", 500);
        }
        try
        {
            TraineeResponse response = new TraineeResponse(old_trainee);
            await RedisCacheHelper.SetObjectAsync<TraineeResponse>($"Trainee:{response.Id}", response, TimeToLiveRedis.Trainee, _logger);
        }
        catch
        {
            _logger.LogWarning("Failed to update in redis");
        }
        return Result<string>.SuccessWithCode($"Put successful for trainee with id: {trainee.Id}", 201);
    }


    public async Task<Result<string>> DeleteById(long id)
    {
        Trainee? trainee;
        try
        {
            trainee = await _repository.GetByIdAsync(id);
        }
        catch
        {
            _logger.LogWarning("Trainee: Problems with database");
            return Result<string>.ServerError("Failed to connect to database..", 500);
        }
        
        if (trainee == null)
        {
            _logger.LogWarning("Trainee: Delete by Id failed - Id not found");
            // return Result<string>.Failure("Id not found");
            return Result<string>.ServerError("Id not found", 404);
        }
        try
        {
            _repository.Delete(trainee);
            await _repository.SaveChangesAsync();
        }
        catch
        {
            _logger.LogWarning("Trainee: Failed to delete in database");
            return Result<string>.ServerError("Failed to delete in the database", 500);
        }
        _logger.LogInformation("Trainee: Delete by Id successful");
        try
        {
            if (await RedisCacheHelper.GetObjectAsync<TraineeResponse>($"Trainee:{id}", _logger) != null)
                await RedisCacheHelper.KeyDelete($"Trainee:{id}", _logger);
        }
        catch
        {
            _logger.LogWarning("Failed to delete in redis");
        }
        // return Result<string>.Success(200);
        return Result<string>.SuccessWithCode($"Trainee with id {id} deleted", 204);
    }

}
