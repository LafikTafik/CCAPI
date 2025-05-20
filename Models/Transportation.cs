namespace CCAPI.Models
{
    public class Transportation
    {
        public int LoadId { get; set; }
        public int VehicleId { get; set; }

        // Навигационные свойства
        public Cargos Load { get; set; } = null!;
        public Vehicle Vehicle { get; set; } = null!;
    }
}