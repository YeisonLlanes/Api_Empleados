using System;
using System.Collections.Generic;

namespace primera_Api.Models;

public partial class Cargos
{
    public int IdCargo { get; set; }

    public int IdDepartamento { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Salario { get; set; }

    public virtual ICollection<Empleados> Empleados { get; set; } = new List<Empleados>();

    public virtual Departamentos IdDepartamentoNavigation { get; set; } = null!;
}
