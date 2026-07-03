using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.db;
using TraineeManagementApi.DTO;

namespace TraineeManagementApi.db;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string?> GetUserPassword(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(t => t.Username == username );
        if (user == null)
        {
            return null;
        }

        return user.PasswordHash;
    }

    public async Task<UserResponse> GetUserDetails(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(t => t.Username == username );
        UserResponse response = new UserResponse()
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role
        };
        return response;
    }

}
