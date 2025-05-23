namespace CCAPI.DTO
{
    public class VehicleDto
    {
        public int ID { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Capacity { get; set; } = string.Empty;
        public string VehicleNum { get; set; } = string.Empty;
        public int DriverId { get; set; }
    }
}
