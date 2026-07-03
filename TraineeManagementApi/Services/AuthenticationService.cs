namespace TraineeManagementApi.Services;

using TraineeManagementApi.db;
using TraineeManagementApi.DTO;

public class AuthenticationService : IAuthenticationService
{
    private readonly IPasswordHelper _passwordHelper;
    private readonly IUserRepository _repository;

    private readonly JwtService _jwtService;

    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(IPasswordHelper passwordHelper, IUserRepository userRepository, JwtService jwtService, ILogger<AuthenticationService> logger)
    {
        _passwordHelper = passwordHelper;
        _repository = userRepository;
        _jwtService = jwtService;
        _logger = logger;
    }

    public async Task<Result<LoginResponse>> Login(LoginRequest request)
    {
        string? password = await _repository.GetUserPassword(request.Username);
        if (password == null)
        {
            _logger.LogWarning("Log in failed: User not found");
            return Result<LoginResponse>.Failure("User not found");
        }
        
        if (_passwordHelper.VerifyPassword(request.Username, password, request.Password)){
            UserResponse user = await _repository.GetUserDetails(request.Username);
            string token = _jwtService.GenerateToken(user.Id, user.Username, user.Role);
            LoginResponse response = new LoginResponse()
            {
                Token = token,
                ExpiresIn = 3600,
                User = user  
            };
            _logger.LogInformation("Log in LogInformation");
            return Result<LoginResponse>.Success(response);
        }
        else
        {
            _logger.LogWarning("Log in failed: Password Incorrect");
            return Result<LoginResponse>.Failure("Incorrect password");
        }

    }

}