using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using primera_Api.Data;
using primera_Api.Models;

namespace primera_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly DbEmpresaContext _context;

        public DepartamentoController(DbEmpresaContext context) 
        {
            _context = context;
        }

        [HttpGet]
        //200
        public async Task<ActionResult<Departamento>> GetAll()
        //Task<ActionResult<List<Departamento>>>
        {
            var Dpto = await _context.Departamentos.ToListAsync(); 
            //return RedirectToAction("Index", "Departamento");
            //return await _context.Departamentos.ToListAsync();
            return Ok(Dpto);
        }

        [HttpGet("{id}")]
        //404 //200
        public async Task<ActionResult<Departamento>> GetDptoById(int id)
        {
            var dpto = await _context.Departamentos.FindAsync(id);
            if(dpto == null)
            {
                return NotFound();
            }
            return dpto;
        }

        [HttpPost]
        //200
        public async Task<ActionResult<Departamento>>CreateDpto([FromBody] Departamento dpto)
        {
            _context.Add(dpto);
            await _context.SaveChangesAsync();

            return Ok(dpto);
        }

        [HttpPut("{id}")]
        //204
        public async Task<ActionResult> UpdateDpto (int id, [FromBody] Departamento dpto)
        {
            if(id != dpto.IdDepartamento)
            {
                return BadRequest();
            }
            _context.Entry(dpto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
                
        }

        [HttpDelete("{id}")]
        //404 //200
        public async Task<IActionResult> DeleteDpto(int id)
        {
            var dpto = await _context.Departamentos.FindAsync(id);
            if(dpto == null)
            {
                return NotFound();
            }
            _context.Departamentos.Remove(dpto);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
