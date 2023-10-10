using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace primera_Api.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string? Nombre { get; set; }

    public int? IdCargo { get; set; }

    public DateTime? FechaIngreso { get; set; }
    
    [JsonIgnore]
    public virtual Cargo? IdCargoNavigation { get; set; }
}
