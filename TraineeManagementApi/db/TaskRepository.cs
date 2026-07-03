using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.db;
using TraineeManagementApi.Models;
using TraineeManagementApi.DTO;

namespace TraineeManagementApi.db;

public class LearningTaskRepository : ILearningTaskRepository
{
    private readonly ApplicationDbContext _context;

    public LearningTaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<LearningTaskResponse>> GetAllAsync(string? search, string? status, int pageNumber, int pageSize)
    {
        IQueryable<LearningTask> query = _context.LearningTasks;
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower().Trim();
            query = query.Where(t =>
                t.Title.ToLower().Contains(search) ||
                t.Description.ToLower().Contains(search) ||
                t.ExpectedTechStack.ToLower().Contains(search)
            );
        }
        if (!string.IsNullOrWhiteSpace(status))
        {
            status = status.ToLower().Trim();
            query = query.Where(t => t.Status.ToLower() == status);
        }

        query = query.Skip((pageNumber-1)*pageSize).Take(pageSize);
        List<LearningTaskResponse> tasks = [];
        foreach (var task in query)
        {
            
            LearningTaskResponse task2 = new LearningTaskResponse(task);
            // Console.WriteLine(task2.Id);
            tasks.Add(task2);
        }
        return tasks;
    }

    public async Task<LearningTask?> GetByIdAsync(long id)
    {
        return await _context.LearningTasks.FindAsync(id);
    }

    public async Task AddAsync(LearningTask task)
    {
        await _context.LearningTasks.AddAsync(task);
    }

    public void Update(LearningTask task)
    {
        _context.LearningTasks.Update(task);
    }

    public void Delete(LearningTask task)
    {
        _context.LearningTasks.Remove(task);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
