using CCAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Transportation
{
    public string ActiveVehicle { get; set; } = string.Empty;

    [ForeignKey("Cargos")]
    public int CargoID { get; set; }

    public Cargos Load { get; set; } = null!;

    [ForeignKey("Vehicle")]
    public int VehicleId { get; set; }

    public Vehicle Vehicle { get; set; } = null!;
}