namespace TraineeManagementApi.Services;

using Microsoft.AspNetCore.Identity;

public interface IPasswordHelper
{
    string GeneratePassword(string username, string password);
    bool VerifyPassword(string username, string hashedpassword, string password);
}

public class PasswordHelper: IPasswordHelper
{
    private readonly IPasswordHasher<string> _passwordHasher;

    public PasswordHelper( IPasswordHasher<string> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string GeneratePassword( string username, string password)
    {
        return _passwordHasher.HashPassword(username, password);
    }

    public bool VerifyPassword(string username, string hashedpassword, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(username, hashedpassword, password);
        switch (result)
        {
            case PasswordVerificationResult.Success:
            case PasswordVerificationResult.SuccessRehashNeeded:
                return true;
            case PasswordVerificationResult.Failed:
                return false;
            default:
                return false;
        }
    }
}