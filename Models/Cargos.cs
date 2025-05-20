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
        // Отношение "один ко многим" с Перевозками
        public ICollection<Transportation> Transportations { get; set; } = new List<Transportation>();

        // Навигационное свойство к заказу
        public Orders Order { get; set; } = null!;
    }
}