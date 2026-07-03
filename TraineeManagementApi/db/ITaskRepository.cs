using TraineeManagementApi.Models;
using TraineeManagementApi.DTO;

using Microsoft.EntityFrameworkCore;

namespace TraineeManagementApi.db;

public interface ILearningTaskRepository
{
    Task<List<LearningTaskResponse>> GetAllAsync(string? search, string? status, int pageNumber, int pageSize);
    Task<LearningTask?> GetByIdAsync(long id);
    Task AddAsync(LearningTask task);
    void Update(LearningTask task);
    void Delete(LearningTask task);
    Task<bool> SaveChangesAsync();
}
