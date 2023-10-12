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
        [ProducesResponseType(StatusCodes.Status200OK)]//200-404
        public async Task<ActionResult<DepartamentoDTO>> GetAll()
        //Task<ActionResult<List<Departamento>>>
        {
            //var Dpto = await _context.Departamentos.ToListAsync();
            //return RedirectToAction("Index", "Departamento");
            //return await _context.Departamentos.ToListAsync();
            //return View("~/Views/Departamento/Index.cshtml", Dpto);

            var dptoDTO = await _context.Departamentos
           .Select(x => dptoToDTO(x))
           .ToListAsync();


            return Ok(dptoDTO);
        }

        [HttpGet("{int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//200-404
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
        [ProducesResponseType(StatusCodes.Status200OK)]//200-201-400
        public async Task<ActionResult<DepartamentoDTO>>CreateDpto([FromBody] DepartamentoDTO dptoDTO)
        {
            var departamento = DTOtoDepartamento(dptoDTO);

            _context.Add(departamento);
            await _context.SaveChangesAsync();

            return Ok(departamento);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDpto (int id, [FromBody] DepartamentoDTO dptoDTO)
        {
            if(id != dptoDTO.IdDepartamento)
            {
                return BadRequest();
            }

            var departamento = await _context.Departamentos.FindAsync(id);

            if (departamento == null)
            {
                return NotFound();
            }

            departamento.Descripcion = dptoDTO.Descripcion;

            _context.Entry(departamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]//204-400-404
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

        /*private bool DepartamentoExists(int id)
        {
            return (_context.Departamentos?.Any(e => e.IdDepartamento == id)).GetValueOrDefault();
        }*/

        private static DepartamentoDTO dptoToDTO(Departamento dpto) =>
          new DepartamentoDTO
          {
              IdDepartamento = dpto.IdDepartamento,
              Descripcion = dpto.Descripcion,
          };

        private static Departamento DTOtoDepartamento(DepartamentoDTO dptoDTO) => new Departamento
        {
            IdDepartamento = dptoDTO.IdDepartamento,
            Descripcion = dptoDTO.Descripcion
        };

    }
}
