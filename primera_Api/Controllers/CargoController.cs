using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using primera_Api.Data;
using primera_Api.Models;

namespace primera_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly DbEmpresaContext _context;
        public CargoController(DbEmpresaContext context) { 
            _context = context;
        }

        [HttpGet]
        //200 - 404
        public async Task<ActionResult<IEnumerable<Cargo>>> GetAll()
        {
            if(_context.Cargos == null)
            {
                return NotFound();
            }
            var cargo = await _context.Cargos.ToListAsync();
            return Ok(cargo);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cargo>> GetCargoById(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            if(cargo == null)
            {
                return NotFound();
            }
            return cargo;

        }

        [HttpPost]
        public async Task<ActionResult<Cargo>> CreateCargo([FromBody] Cargo cargo)
        {
            var dpto = await _context.Departamentos.FindAsync(cargo.IdDepartamento);
            if (dpto == null)
            {
                return NotFound("Departamento no existe");
            }
            _context.Add(cargo);
            await _context.SaveChangesAsync();
            return Ok(cargo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCargo(int id, Cargo cargo)
        {
            if(id != cargo.IdCargo)
            {
                return BadRequest("Cargo No coincide");
            }

            var dpto = await _context.Departamentos.FindAsync(cargo.IdDepartamento);
            if (dpto == null)
            {
                return NotFound("Departamento no existe");
            }

            _context.Entry(cargo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrago(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            _context.Cargos.Remove(cargo);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
