namespace TraineeManagementApi.Services;

using TraineeManagementApi.DTO;

using TraineeManagementApi.Models;

using TraineeManagementApi.db;


public class LearningTaskService : ILearningTaskService
{
    private readonly ILearningTaskRepository _repository;
    private readonly ILogger<LearningTaskService> _logger;

    public LearningTaskService(ILearningTaskRepository repository, ILogger<LearningTaskService> logger)
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task<Result<SearchResponse>> GetAll(SearchQuery searchQuery)
    {
        var tasks = await _repository.GetAllAsync(searchQuery.search, searchQuery.status, searchQuery.pageNumber, searchQuery.pageSize);
        if (tasks == null)
        {
            _logger.LogWarning("Task: Get All failed: No tasks found with given keywords");
            return Result<SearchResponse>.Failure("");
        }
        SearchResponse response = new SearchResponse(tasks, searchQuery.pageNumber, searchQuery.pageSize);
        _logger.LogInformation("Task: Get All LogInformation");
        return Result<SearchResponse>.Success(response);
    }

    public async Task<Result<LearningTaskResponse>> GetById(long id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null)
        {
            _logger.LogWarning("Task: Get with Id failed: No task found with given Id");
            return Result<LearningTaskResponse>.Failure("Task not found.");
        }
        LearningTaskResponse response = new LearningTaskResponse(task);
        _logger.LogInformation("Task: Get with Id successful");
        return Result<LearningTaskResponse>.Success(response);
    }

    public async Task<Result<long>> PostById( CreateLearningTaskRequest task)
    {
        LearningTask task1 = new LearningTask(task);
        await _repository.AddAsync(task1);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Task: Post successful");
        return Result<long>.Success(200);
    }

    public async Task<Result<long>> PutById(UpdateLearningTaskRequest task)
    {
        var old_task = await _repository.GetByIdAsync(task.Id);
        if (old_task == null)
        {
            _logger.LogWarning("Task: Put by Id failed - Id not found");
            return Result<long>.Failure("Task not found.");
        }
        try
        {
            old_task.Title = task.Title;
            old_task.Description = task.Description;
            old_task.ExpectedTechStack = task.ExpectedTechStack;
            old_task.DueDate = task.DueDate;
            old_task.Status = task.Status;
            old_task.UpdatedDate = DateTime.UtcNow;
            _repository.Update(old_task);
            await _repository.SaveChangesAsync();
            _logger.LogInformation("Task: Put by Id successful");
            return Result<long>.Success(200);
            
        }
        catch
        {
            _logger.LogWarning("Task: Put by Id failed - Invalid Data");
            return Result<long>.Failure("Invalid data");
        }
    }


    public async Task<Result<long>> DeleteById(long id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null)
        {
            _logger.LogWarning("Task: Delete by Id failed - Id not found");
            return Result<long>.Failure("Id not found");
        }
        _repository.Delete(task);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Task: Delete by Id successful");
        return Result<long>.Success(200);
    }

}
