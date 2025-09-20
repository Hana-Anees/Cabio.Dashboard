namespace Cabio.Dashboard.Application.Dtos.Drivers
{
    public class UpdateDriverDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public bool SafeGuarding { get; set; }
        public bool DisabilityAwareness { get; set; }
        public string VehicleModel { get; set; } = string.Empty;
        public string VehicleRegistration { get; set; } = string.Empty;
        public DateTime MOT { get; set; }
        public DateTime RoadTax { get; set; }
    }
}
