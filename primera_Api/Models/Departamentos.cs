using System;
using System.Collections.Generic;

namespace primera_Api.Models;

public partial class Departamentos
{
    public int IdDepartamento { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Cargos> Cargos { get; set; } = new List<Cargos>();
}
