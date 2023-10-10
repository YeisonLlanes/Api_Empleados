using primera_Api.Models;

namespace primera_Api.Data
{
    public class CargoDTO
    {
        public int IdCargo { get; set; }

        public int IdDepartamento { get; set; }

        public string? Descripcion { get; set; }

        public decimal? Salario { get; set; }

    }
}
