using TraineeManagementApi.Models;
using TraineeManagementApi.DTO;

using Microsoft.EntityFrameworkCore;

namespace TraineeManagementApi.db;

public interface IMentorRepository
{
    Task<List<MentorResponse>> GetAllAsync(string? search, string? status, int pageNumber, int pageSize);
    Task<Mentor?> GetByIdAsync(long id);
    Task AddAsync(Mentor mentor);
    void Update(Mentor mentor);
    void Delete(Mentor mentor);
    Task<bool> SaveChangesAsync();
}
