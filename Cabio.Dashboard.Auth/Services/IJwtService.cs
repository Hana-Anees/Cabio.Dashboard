namespace Cabio.Dashboard.Auth
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
    }
}
