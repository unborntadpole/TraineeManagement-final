using TraineeManagementApi.Models;
using TraineeManagementApi.DTO;

using Microsoft.EntityFrameworkCore;

namespace TraineeManagementApi.db;

public interface ITraineeRepository
{
    Task<List<TraineeResponse>> GetAllAsync(string? search, string? status, int pageNumber, int pageSize);
    Task<Trainee?> GetByIdAsync(long id);
    Task AddAsync(Trainee trainee);
    void Update(Trainee trainee);
    void Delete(Trainee trainee);
    Task<bool> SaveChangesAsync();
}
