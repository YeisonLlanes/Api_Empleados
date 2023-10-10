using System;
using System.Collections.Generic;

namespace primera_Api.Models;

public partial class Empleados
{
    public int IdEmpleado { get; set; }

    public string? Nombre { get; set; }

    public int? IdCargo { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public virtual Cargos? IdCargoNavigation { get; set; }
}
