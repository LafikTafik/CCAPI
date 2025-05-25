using CCAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCAPI.DTO.defaultt
{
    public class OrderDto
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

    }
}
