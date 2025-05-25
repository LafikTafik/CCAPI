using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CCAPI.Models
{
    public class Vehicle
    {
        [Key]
        public int ID { get; set; }

        [Column("Type")]
        public string Type { get; set; } = string.Empty;

        [Column("Capacity")]
        public string Capacity { get; set; } = string.Empty;

        [Column("DriverID")]
        public int DriverId { get; set; }

        [Column("VehicleNum")]
        public string VehicleNum { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }



        public ICollection<Transportation> Transportations { get; set; } = new List<Transportation>();

        public Driver Driver { get; set; } = null!;
    }
}