using System;
using System.Collections.Generic;

namespace primera_Api.Models;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();
}
