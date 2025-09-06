namespace Cabio.Dashboard.Api.Models
{
    public record DriverDto(
        int Id,
        string Name,
        string Address,
        DateTime DateOfBirth,
        string LicenseNumber,
        string Contact,
        bool SafeGuarding,
        bool DisabilityAwareness
    );

    public record CreateDriverRequest(
        string Name,
        string Address,
        DateTime DateOfBirth,
        string LicenseNumber,
        string Contact,
        bool SafeGuarding,
        bool DisabilityAwareness
    );
}
