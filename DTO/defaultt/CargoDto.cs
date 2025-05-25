namespace CCAPI.DTO.defaultt
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
