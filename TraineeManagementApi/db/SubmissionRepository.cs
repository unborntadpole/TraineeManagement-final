namespace TraineeManagementApi.db;
using TraineeManagementApi.DTO;
using TraineeManagementApi.Models;
public class SubmissionRepository
{
    private readonly ApplicationDbContext _context;

    public SubmissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SubmissionDTO>> GetAllAsync()
    {
        var submissions = _context.Submissions;
        List<SubmissionDTO> submissions2 = [];
        foreach (var submission in submissions)
        {
            SubmissionDTO submission2 = new SubmissionDTO(submission);
            submissions2.Add(submission2);
        }
        return submissions2;
    }

    public async Task<Submission?> GetByIdAsync(long id)
    {
        return await _context.Submissions.FindAsync(id);
    }

    public async Task AddAsync(Submission submission)
    {
        await _context.Submissions.AddAsync(submission);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}