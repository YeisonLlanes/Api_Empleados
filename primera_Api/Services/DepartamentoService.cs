using Microsoft.EntityFrameworkCore;
using primera_Api.Data;
using primera_Api.Models;

namespace primera_Api.Services
{
    public class DepartamentoService: IDepartamento
    {
        private readonly DbEmpresaContext _dbContext;

        public DepartamentoService(DbEmpresaContext context)
        {
            _dbContext = context;
        }

        public async Task<List<DepartamentoDTO>> GetDepartamentos()
        {
            var dptoDTO = await _dbContext.Departamentos
           .Select(x => dptoToDTO(x))
           .ToListAsync();

            return dptoDTO.ToList();

        }
        
        public async Task<DepartamentoDTO> GetById(int idDepartamentoDTO)
        {
            DepartamentoDTO departamento = new DepartamentoDTO();
            var dpto = await _dbContext.Departamentos.FindAsync(idDepartamentoDTO);
            
            if (dpto == null)
            {
                return departamento;
            }

            return dptoToDTO(dpto);
        }

        public async Task<DepartamentoDTO> Create(DepartamentoDTO departamentoDTO)
        {
            var departamento = DTOtoDepartamento(departamentoDTO);

            _dbContext.Add(departamento);
            await _dbContext.SaveChangesAsync();

            return dptoToDTO(departamento);
        }

        public async Task<DepartamentoDTO> Update(int idDepartamentoDTO, DepartamentoDTO departmentoDTO)
        {
            var departamento = await GetById(idDepartamentoDTO);
           
            if (departamento.IdDepartamento == 0 || departamento.Descripcion == null)
            {
                return departamento;
            }

            departamento.Descripcion = departmentoDTO.Descripcion;
            var dpto = DTOtoDepartamento(departamento);
            _dbContext.ChangeTracker.Clear();//--> Opcion sustituye AsNoTRacking() con EF, limpia el objeto que esta siendo tracking(Validar mejor opcion)
            _dbContext.Entry(dpto).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return dptoToDTO(dpto);

        }

        public async Task<bool> Delete(int idDepartamentoDTO)
        {
            var departamento = await GetById(idDepartamentoDTO);

            if (departamento.IdDepartamento == 0 || departamento.Descripcion == null)
            {
                return false;
            }
            var dpto = DTOtoDepartamento(departamento);
            
            _dbContext.ChangeTracker.Clear();//--> Opcion sustituye AsNoTRacking() con EF, limpia el objeto que esta siendo tracking(Validar mejor opcion)

            _dbContext.Departamentos.Remove(dpto);
            await _dbContext.SaveChangesAsync();
            return true;
        }

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
