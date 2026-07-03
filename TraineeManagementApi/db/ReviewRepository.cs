namespace TraineeManagementApi.db;
using TraineeManagementApi.DTO;
using TraineeManagementApi.Models;
public class ReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReviewDTO>> GetAllAsync()
    {
        var reviews = _context.Reviews;
        List<ReviewDTO> reviews2 = [];
        foreach (var review in reviews)
        {
            ReviewDTO review2 = new ReviewDTO(review);
            reviews2.Add(review2);
        }
        return reviews2;
    }

    public async Task<Review?> GetByIdAsync(long id)
    {
        return await _context.Reviews.FindAsync(id);
    }

    public async Task AddAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}