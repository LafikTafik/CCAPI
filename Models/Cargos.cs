using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CCAPI.Models
{
    public class Cargos
    {
        [Key]
        public int ID { get; set; }

        public string Weight { get; set; } = string.Empty;
        public string Dimensions { get; set; } = string.Empty;
        public string Descriptions { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }


        // Многие ко многим: Cargo ↔ Orders через CargoOrders
        public ICollection<CargoOrders> Orders { get; set; } = new List<CargoOrders>();


    }
}