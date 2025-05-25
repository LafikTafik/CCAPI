using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCAPI.Models
{
    public class Client
    {
        [Key]
        public int ID { get; set; }

        [Column("Name")]
        public string Name { get; set; } = string.Empty;

        [Column("Surname")]
        public string Surname { get; set; } = string.Empty;

        [Column("Phone")]
        public string Phone { get; set; } = string.Empty;

        [Column("Email")]
        public string Email { get; set; } = string.Empty;

        [Column("Adress")]
        public string Address { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }


        public ICollection<Orders> Orders { get; set; } = new List<Orders>();
    }
}