namespace CCAPI.DTO.deleted
{
    public class DeletedTransportationDto
    {
        public int ActiveVehicle { get; set; }
        public int LoadId { get; set; }
        public int VehicleId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
