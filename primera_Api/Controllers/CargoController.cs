using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CargoDTO>> GetAll()
        {
            if(_context.Cargos == null)
            {
                return NotFound();
            }
            var cargo = await _context.Cargos.ToListAsync();
            return Ok(cargo);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CargoDTO>> GetCargoById(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            if(cargo == null)
            {
                return NotFound();
            }
            return cargoToDTO(cargo);

        }

        [HttpPost]
        public async Task<ActionResult<CargoDTO>> CreateCargo([FromBody] CargoDTO cargo)
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
        public async Task<ActionResult> UpdateCargo(int id, CargoDTO cargo)
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

        private static CargoDTO cargoToDTO(Cargo cargo) =>
          new CargoDTO
          {
              IdCargo = cargo.IdCargo,
              IdDepartamento = cargo.IdDepartamento,
              Descripcion = cargo.Descripcion,
              Salario = cargo.Salario
          };

    }
}
