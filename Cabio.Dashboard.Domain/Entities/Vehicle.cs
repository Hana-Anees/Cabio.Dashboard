namespace Cabio.Dashboard.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }            
        public string Model { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public DateTime MOTExpiry { get; set; }
        public DateTime RoadTaxExpiry { get; set; }

        // Foreign Key to Driver
        public int DriverId { get; set; }
        public Driver Driver { get; set; } = null!;
    }
}
