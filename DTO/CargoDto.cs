using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCAPI.DTO
{
    public class CargoDto
    {
        public int ID { get; set; }
        public int? OrderID { get; set; }
        public string Weight { get; set; } = string.Empty;
        public string Dimensions { get; set; } = string.Empty;
        public string Descriptions { get; set; } = string.Empty;
    }
}
