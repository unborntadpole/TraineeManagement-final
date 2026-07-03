namespace TraineeManagementApi.Services;

using TraineeManagementApi.DTO;
using TraineeManagementApi.Models;
using TraineeManagementApi.db;

public class ReviewService
{
    private readonly ReviewRepository _repository;
    private readonly ILogger<ReviewService> _logger;

    public ReviewService(ReviewRepository repository, ILogger<ReviewService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<List<ReviewDTO>>> GetAll()
    {
        List<ReviewDTO> reviews;
        try
        {
            reviews = await _repository.GetAllAsync();
        }
        catch
        {
            _logger.LogWarning("Review: Get All failed: Failed to connect to database");
            return Result<List<ReviewDTO>>.ServerError("Failed to connect to database..", 500);
        }
        if (reviews == null)
        {
            _logger.LogWarning("Review: Get All failed: No review found");
            return Result<List<ReviewDTO>>.Failure("");
        }
        _logger.LogInformation("Review: Get All LogInformation");
        return Result<List<ReviewDTO>>.Success(reviews);
    }
    
    public async Task<Result<ReviewDTO>> GetById(long id)
    {
        Review review;
        try
        {
            review = await _repository.GetByIdAsync(id);
        }
        catch
        {
            _logger.LogWarning("Review: Get by Id failed: Failed to connect to database");
            return Result<ReviewDTO>.ServerError("Failed to connect to database..", 500);
        }
        if (review == null)
        {
            _logger.LogWarning("Review: Get by Id failed: No review found with given id");
            return Result<ReviewDTO>.Failure("No review with given id");
        }
        _logger.LogInformation("Review: Get By Id executed successfully");
        ReviewDTO reviewDTO = new ReviewDTO(review);
        return Result<ReviewDTO>.Success(reviewDTO);
    }

    public async Task<Result<long>> PostById( ReviewDTO reviewDTO)
    {
        Review review = new Review(reviewDTO);
        try
        {
            await _repository.AddAsync(review);
            await _repository.SaveChangesAsync();
        }
        catch
        {
            _logger.LogWarning("Review: Post failed. Database problems");
            return Result<long>.ServerError("Post failed due to database problems",500);
        }
        _logger.LogInformation("Review: Post successful");
        return Result<long>.Success(200);
    }


}