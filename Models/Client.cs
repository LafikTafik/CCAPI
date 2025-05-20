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

        // Указываем, что внешний ключ в таблице Order — это "IDClient"
        [InverseProperty("Client")]
        [ForeignKey("IDClient")]  // <-- Вот здесь явно указываем, какой столбец используется как внешний ключ
        public ICollection<Orders> Orders { get; set; } = new List<Orders>();
    }
}