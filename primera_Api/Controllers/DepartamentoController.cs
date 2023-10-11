using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using primera_Api.Data;
using primera_Api.Models;

namespace primera_Api.Controllers
{
    [Route("api/[controller]")]
    //public class DepartamentoController : ControllerBase
    //Atributo [ApiController] en varios controladores https://learn.microsoft.com/es-mx/aspnet/core/web-api/?view=aspnetcore-7.0
    public class DepartamentoController : primerController
    {
        private readonly DbEmpresaContext _context;

        public DepartamentoController(DbEmpresaContext context) 
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DepartamentoDTO>> GetAll()
        //Task<ActionResult<List<Departamento>>>
        {
            var Dpto = await _context.Departamentos.ToListAsync();
            //return RedirectToAction("Index", "Departamento");
            //return await _context.Departamentos.ToListAsync();
            //return View();
            return Ok(Dpto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartamentoDTO>> GetDptoById(int id)
        {
            var dpto = await _context.Departamentos.FindAsync(id);
            if(dpto == null)
            {
                return NotFound();
            }
            return dptoToDTO(dpto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DepartamentoDTO>>CreateDpto([FromBody] DepartamentoDTO dpto)
        {
            _context.Add(dpto);
            await _context.SaveChangesAsync();

            return Ok(dpto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDpto (int id, [FromBody] DepartamentoDTO dpto)
        {
            if(id != dpto.IdDepartamento)
            {
                return BadRequest();
            }

            _context.Entry(dpto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            
            return NoContent();
                
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDpto(int id)
        {
            var dpto = await _context.Departamentos.FindAsync(id);
            
            if(dpto == null)
            {
                return NotFound();
            }

            _context.Departamentos.Remove(dpto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool DepartamentoExists(int id)
        {
            return (_context.Departamentos?.Any(e => e.IdDepartamento == id)).GetValueOrDefault();
        }

        private static DepartamentoDTO dptoToDTO(Departamento dpto) =>
          new DepartamentoDTO
          {
              IdDepartamento = dpto.IdDepartamento,
              Descripcion = dpto.Descripcion,
          };

    }
}
