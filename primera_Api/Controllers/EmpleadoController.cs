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
            //var empleado = await _context.Empleados.ToListAsync();

            var empleadoDTO = await _context.Empleados
            .Select(x => empleadoToDTO(x))
            .ToListAsync();

            return Ok(empleadoDTO);
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
        public async Task<ActionResult<EmpleadoDTO>> CreateEmpleado([FromBody] EmpleadoDTO empleadoDTO)
        {
            var cargo = await _context.Cargos.FindAsync(empleadoDTO.IdCargo);
            if (cargo == null)
            {
                return NotFound("Cargo No existe");
            }

            var empleado = DTOtoEmpleado(empleadoDTO);
           
            _context.Add(empleado);
            await _context.SaveChangesAsync();
            return Ok(empleado);
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateEmpleado(int id, [FromBody] EmpleadoDTO empleadoDTO)
        {
            if(id != empleadoDTO.IdEmpleado)
            {
                return BadRequest();
            }

            var cargo = await _context.Cargos.FindAsync(empleadoDTO.IdCargo);
            if(cargo == null)
            {
                return NotFound("Cargo No existe");
            }

            var empleado = await _context.Empleados.FindAsync(id);
            
            if (empleado == null)
            {
                return NotFound();
            }

            empleado.IdCargo = empleadoDTO.IdCargo;
            empleado.Nombre = empleadoDTO.Nombre;

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

        private static Empleado DTOtoEmpleado(EmpleadoDTO empleadoDTO) => new Empleado
        {
            IdEmpleado = empleadoDTO.IdEmpleado,
            Nombre = empleadoDTO.Nombre,
            IdCargo = empleadoDTO.IdCargo,
            FechaIngreso = empleadoDTO.FechaIngreso
        };


    }
}
