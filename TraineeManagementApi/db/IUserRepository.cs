using Microsoft.EntityFrameworkCore;
using TraineeManagementApi.DTO;

namespace TraineeManagementApi.db;

public interface IUserRepository
{
    Task<string?> GetUserPassword(string username);
    Task<UserResponse> GetUserDetails(string username);
}
