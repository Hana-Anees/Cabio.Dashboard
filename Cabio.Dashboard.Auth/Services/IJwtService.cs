namespace Cabio.Dashboard.Auth.Services
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
    }
}
