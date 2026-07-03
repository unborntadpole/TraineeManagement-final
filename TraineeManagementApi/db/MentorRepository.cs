using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.db;
using TraineeManagementApi.Models;
using TraineeManagementApi.DTO;

namespace TraineeManagementApi.db;

public class MentorRepository : IMentorRepository
{
    private readonly ApplicationDbContext _context;

    public MentorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MentorResponse>> GetAllAsync(string? search, string? status, int pageNumber, int pageSize)
    {
        IQueryable<Mentor> query = _context.Mentors;
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower().Trim();
            query = query.Where(t =>
                t.FirstName.ToLower().Contains(search) ||
                t.LastName.ToLower().Contains(search) ||
                t.Email.ToLower().Contains(search) ||
                t.Expertise.ToLower().Contains(search));
        }
        if (!string.IsNullOrWhiteSpace(status))
        {
            status = status.ToLower().Trim();
            query = query.Where(t => t.Status.ToLower() == status);
        }

        query = query.Skip((pageNumber-1)*pageSize).Take(pageSize);
        List<MentorResponse> mentors = [];
        foreach (var mentor in query)
        {
            MentorResponse mentor2 = new MentorResponse(mentor);
            mentors.Add(mentor2);
        }
        return mentors;
    }

    public async Task<Mentor?> GetByIdAsync(long id)
    {
        return await _context.Mentors.FindAsync(id);
    }

    public async Task AddAsync(Mentor mentor)
    {
        await _context.Mentors.AddAsync(mentor);
    }

    public void Update(Mentor mentor)
    {
        _context.Mentors.Update(mentor);
    }

    public void Delete(Mentor mentor)
    {
        _context.Mentors.Remove(mentor);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
