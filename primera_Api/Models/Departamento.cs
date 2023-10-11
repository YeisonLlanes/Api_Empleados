using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace primera_Api.Models;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public string? Descripcion { get; set; }

    [JsonIgnore]
    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();
}
