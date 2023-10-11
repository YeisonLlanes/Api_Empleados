using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using primera_Api.Data;
using primera_Api.Models;

namespace primera_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmpleadoController : ControllerBase
    {
        private readonly DbEmpresaContext _context;

        public EmpleadoController(DbEmpresaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmpleadoDTO>> GetAll()
        {
            if(_context.Empleados == null)
            {
                return NotFound();
            }
            var empleado = await _context.Empleados.ToListAsync();

            return Ok(empleado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoDTO>> GetById(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if(empleado == null)
            {
                return NotFound();
            }

            return empleadoToDTO(empleado);

        }

        [HttpPost]
        public async Task<ActionResult<EmpleadoDTO>> CreateEmpleado([FromBody] EmpleadoDTO empleado)
        {
            var cargo = await _context.Cargos.FindAsync(empleado.IdCargo);
            if (cargo == null)
            {
                return NotFound("Cargo No existe");
            }

            _context.Add(empleado);
            await _context.SaveChangesAsync();
            return Ok(empleado);
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateEmpleado(int id, [FromBody] EmpleadoDTO empleado)
        {
            if(id != empleado.IdEmpleado)
            {
                return BadRequest();
            }

            var cargo = await _context.Cargos.FindAsync(empleado.IdCargo);
            if(cargo == null)
            {
                return NotFound("Cargo No existe");
            }

            _context.Entry(empleado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if(empleado == null)
            {
                return NotFound();
            }
            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private static EmpleadoDTO empleadoToDTO(Empleado empleado) =>
         new EmpleadoDTO
         {
             IdEmpleado = empleado.IdEmpleado,
             Nombre = empleado.Nombre,
             IdCargo = empleado.IdCargo,
             FechaIngreso = empleado.FechaIngreso
         };


    }
}
