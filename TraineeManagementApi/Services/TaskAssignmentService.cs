namespace TraineeManagementApi.Services;

using TraineeManagementApi.DTO;
using TraineeManagementApi.Models;
using TraineeManagementApi.db;
using TraineeManagementApi.Constants;

public class TaskAssignmentService
{
    private readonly TaskAssignmentRepository _repository;
    private readonly ILogger<TaskAssignmentService> _logger;

    public TaskAssignmentService(TaskAssignmentRepository repository, ILogger<TaskAssignmentService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<List<TaskAssignmentDTO>>> GetAll()
    {
        List<TaskAssignmentDTO> taskAssgns;
        try
        {
            taskAssgns = await _repository.GetAllAsync();
        }
        catch
        {
            _logger.LogWarning("Task Assignment: Get All failed: Failed to connect to database");
            return Result<List<TaskAssignmentDTO>>.ServerError("Failed to connect to database..", 500);
        }
        if (taskAssgns == null)
        {
            _logger.LogWarning("Task Assignment: Get All failed: No task assignments found");
            return Result<List<TaskAssignmentDTO>>.Failure("");
        }
        _logger.LogInformation("Task Assignment: Get All LogInformation");
        return Result<List<TaskAssignmentDTO>>.Success(taskAssgns);
    }
    
    public async Task<Result<TaskAssignmentDTO>> GetById(long id)
    {
        TaskAssignment? taskAssg;
        try{
            TaskAssignmentDTO? redisResponse = await RedisCacheHelper.GetObjectAsync<TaskAssignmentDTO>($"TaskAssignment:{id}", _logger);
            if(redisResponse != null)
            {
                return Result<TaskAssignmentDTO>.SuccessWithCode(redisResponse, 200);
            }
        }
        catch(Exception e)
        {
            _logger.LogWarning($"Redis get failed.\n{e}");
        }
        try
        {
            taskAssg = await _repository.GetByIdAsync(id);
        }
        catch
        {
            _logger.LogWarning("Task Assignment: Get by Id failed: Failed to connect to database");
            return Result<TaskAssignmentDTO>.ServerError("Failed to connect to database..", 500);
        }
        if (taskAssg == null)
        {
            _logger.LogWarning("Task Assignment: Get by Id failed: No task assignment found with given id");
            return Result<TaskAssignmentDTO>.Failure("No task assignment with given id");
        }
        _logger.LogInformation("Task Assignment: Get By Id executed successfully");
        TaskAssignmentDTO taskAssignmentDTO = new TaskAssignmentDTO(taskAssg);
        try
        {
            await RedisCacheHelper.SetObjectAsync<TaskAssignmentDTO>($"TaskAssignment:{id}", taskAssignmentDTO, TimeToLiveRedis.TaskAssignment, _logger);
        }
        catch
        {
            _logger.LogWarning("Redis set failed");
        }
        return Result<TaskAssignmentDTO>.Success(taskAssignmentDTO);
    }

    public async Task<Result<long>> PostById( TaskAssignmentDTO taskAssignmentDTO)
    {
        TaskAssignment taskAssignment = new TaskAssignment(taskAssignmentDTO);
        try
        {
            await _repository.AddAsync(taskAssignment);
            await _repository.SaveChangesAsync();
        }
        catch
        {
            _logger.LogWarning("Task Assignment: Post failed. Database problems");
            return Result<long>.ServerError("Post failed due to database problems",500);
        }
        try
        {
            TaskAssignmentDTO tDTO = new TaskAssignmentDTO(taskAssignment);
            await RedisCacheHelper.SetObjectAsync<TaskAssignmentDTO>($"TaskAssignment:{tDTO.Id}", tDTO, TimeToLiveRedis.TaskAssignment, _logger);
        }
        catch
        {
            _logger.LogWarning("Redis set failed");
        }
        _logger.LogInformation("Task Assignment: Post successful");
        return Result<long>.Success(200);
    }

    public async Task<Result<long>> PutById(string status, long id)
    {
        TaskAssignment? taskAssg;
        try
        {
            taskAssg = await _repository.GetByIdAsync(id);
        }
        catch
        {
            _logger.LogWarning("Task Assignment: Put by Id failed - database issue");
            return Result<long>.ServerError("Put failed due to database connection issue.", 500);
        }
        if (taskAssg == null)
        {
            _logger.LogWarning("Task Assignment: Put by Id failed - Id not found");
            return Result<long>.Failure("Task assignment with given id not found.");
        }
        try
        {
            taskAssg.Status = status;
            await _repository.Update(taskAssg);
            await _repository.SaveChangesAsync();
            _logger.LogInformation("Task Assignment: Put by Id successful");

            
        }
        catch
        {
            _logger.LogWarning("Task Assignment: Put by Id failed - database connection issue");
            return Result<long>.ServerError("Connection failed so did put operation", 500);
        }
        try
        {
            TaskAssignmentDTO taskAssignmentDTO = new TaskAssignmentDTO(taskAssg);
            await RedisCacheHelper.SetObjectAsync<TaskAssignmentDTO>($"TaskAssignment:{taskAssignmentDTO.Id}", taskAssignmentDTO, TimeToLiveRedis.TaskAssignment, _logger);
        }
        catch
        {
            _logger.LogWarning("Failed to update in redis");
        }
        return Result<long>.Success(200);
    }


}