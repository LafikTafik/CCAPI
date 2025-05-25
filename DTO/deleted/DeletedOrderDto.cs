using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CCAPI.DTO.deleted
{
    public class DeletedOrderDto
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

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
