namespace TraineeManagementApi.Services;

using TraineeManagementApi.DTO;

public interface IAuthenticationService
{
    Task<Result<LoginResponse>> Login(LoginRequest request);
}