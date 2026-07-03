using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.db;
using TraineeManagementApi.Models;
using TraineeManagementApi.DTO;

namespace TraineeManagementApi.db;

public class TraineeRepository : ITraineeRepository
{
    private readonly ApplicationDbContext _context;

    public TraineeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TraineeResponse>> GetAllAsync(string? search, string? status, int pageNumber, int pageSize)
    {
        IQueryable<Trainee> query = _context.Trainees;
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower().Trim();
            query = query.Where(t =>
                t.FirstName.ToLower().Contains(search) ||
                t.LastName.ToLower().Contains(search) ||
                t.Email.ToLower().Contains(search) ||
                t.TechStack.ToLower().Contains(search));
        }
        if (!string.IsNullOrWhiteSpace(status))
        {
            status = status.ToLower().Trim();
            query = query.Where(t => t.Status.ToLower() == status);
        }

        query = query.Skip((pageNumber-1)*pageSize).Take(pageSize);
        List<TraineeResponse> trainees = [];
        foreach (var trainee in query)
        {
            TraineeResponse trainee2 = new TraineeResponse(trainee);
            trainees.Add(trainee2);
        }
        return trainees;
    }

    public async Task<Trainee?> GetByIdAsync(long id)
    {
        return await _context.Trainees.FindAsync(id);
    }

    public async Task AddAsync(Trainee trainee)
    {
        await _context.Trainees.AddAsync(trainee);
    }

    public void Update(Trainee trainee)
    {
        _context.Trainees.Update(trainee);
    }

    public void Delete(Trainee trainee)
    {
        _context.Trainees.Remove(trainee);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
