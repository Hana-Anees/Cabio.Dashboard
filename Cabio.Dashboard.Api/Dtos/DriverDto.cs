namespace Cabio.Dashboard.Api.Dtos
{
    public class DriverDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
    }

    public class CreateDriverDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public bool SafeGuarding { get; set; }
        public bool DisabilityAwareness { get; set; }
    }
}
