using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
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
        public async Task<ActionResult<IEnumerable<Empleado>>> GetAll()
        {
            if(_context.Empleados == null)
            {
                return NotFound();
            }
            
            return await _context.Empleados.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetById(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if(empleado == null)
            {
                return NotFound();
            }

            return empleado;

        }

        [HttpPost]
        public async Task<ActionResult<Empleado>> CreateEmpleado([FromBody] Empleado empleado)
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
        public async Task<ActionResult> UpdateEmpleado(int id, [FromBody] Empleado empleado)
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


    }
}
