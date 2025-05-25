using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCAPI.Models
{
    public class Transportation
    {
        [Key]
        [Column("ActiveVehicle")]
        public int ActiveVehicle { get; set; } 

        [Column("CargoID")]
        public int CargoID { get; set; }

        [Column("VehicleId")]
        public int VehicleId { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }


        // Навигационные свойства
        [ForeignKey("CargoID")]
        public Cargos Load { get; set; } = null!;

        [ForeignKey("VehicleId")]
        public Vehicle? Vehicle { get; set; }
    }
}