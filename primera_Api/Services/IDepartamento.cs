using Microsoft.AspNetCore.Mvc;
using primera_Api.Models;
using primera_Api.Data;


namespace primera_Api.Services
{
    public interface IDepartamento
    {

        Task<List<DepartamentoDTO>> GetDepartamentos();

        Task<DepartamentoDTO> GetById(int idDepartamentoDTO);

        Task<DepartamentoDTO> Create(DepartamentoDTO departamentoDTO);

        Task<DepartamentoDTO> Update(int idDepartamentoDTO,  DepartamentoDTO departmentoDTO);

        Task<bool> Delete(int idDepartamentoDTO);



    }
}
