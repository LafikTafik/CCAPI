using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCAPI.Models
{
    [Table("Order")]
    public class Orders
    {
        [Key]
        public int ID { get; set; }

        [Column("IDClient")]
        public int? IDClient { get; set; }

        [Column("Date")]
        public DateTime? Date { get; set; }

        [Column("Status")]
        public string? Status { get; set; }

        [Column("Price")]
        public decimal? Price { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public Client? Client { get; set; }
        public ICollection<Cargos>? Cargos { get; set; }

    }
}