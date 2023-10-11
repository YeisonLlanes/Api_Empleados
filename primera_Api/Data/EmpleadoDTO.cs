namespace primera_Api.Data
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }

        public string? Nombre { get; set; }

        public int? IdCargo { get; set; }

        public DateTime? FechaIngreso { get; set; }
    }
}
