using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCAPI.Models
{
    public class Transportation
    {
        [Key]
        [Column("ActiveVehicle")]
        public int ActiveVehicle { get; set; } // Теперь это первичный ключ

        [Column("CargoID")]
        public int CargoID { get; set; }

        [Column("VehicleID")]
        public int VehicleID { get; set; }

        // Навигационные свойства
        [ForeignKey("CargoID")]
        public Cargos? Load { get; set; }

        [ForeignKey("VehicleID")]
        public Vehicle? Vehicle { get; set; }
    }
}