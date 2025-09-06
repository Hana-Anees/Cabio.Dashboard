namespace Cabio.Dashboard.Domain.Entities
{
    public class Driver
    {
        public int Id { get; set; }              
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public bool SafeGuarding { get; set; }
        public bool DisabilityAwareness { get; set; }

        // Navigation property (One Driver -> Many Vehicles)
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
