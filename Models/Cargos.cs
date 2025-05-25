using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CCAPI.Models
{
    public class Cargos
    {
        [Key]
        public int ID { get; set; }

        [Column("OrderID")]
        public int? OrderId { get; set; }

        [Column("Weight")]
        public string Weight { get; set; } = string.Empty;

        [Column("Dimensions")]
        public string Dimensions { get; set; } = string.Empty;

        [Column("Descriptions")]
        public string Descriptions { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }



        // Отношение "один ко многим" с Перевозками
        [ForeignKey("OrderId")]
        public Orders? Order { get; set; }

    
    }
}