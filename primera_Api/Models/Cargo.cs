using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace primera_Api.Models;

public partial class Cargo
{
    public int IdCargo { get; set; }

    public int IdDepartamento { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Salario { get; set; }

    [JsonIgnore]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    [JsonIgnore]
    public virtual Departamento? IdDepartamentoNavigation { get; set; } = null!;
}
