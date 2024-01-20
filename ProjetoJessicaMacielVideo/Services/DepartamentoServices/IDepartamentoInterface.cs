using Microsoft.AspNetCore.Mvc;
using ProjetoJessicaMacielVideo.Dto.DepartamentoDto;
using ProjetoJessicaMacielVideo.Models;

namespace ProjetoJessicaMacielVideo.Services.DepartamentoServices
{
    public interface IDepartamentoInterface
    {
        Task<ResponseModel<List<DepartamentoModel>>> ListarDepartamentos();
        Task<ResponseModel<DepartamentoModel>> ListarDepartamentoPorId(int id);
        Task<ResponseModel<List<DepartamentoModel>>> CriarDepartamento(DepartamentoCriacaoDto departamentoCriacaoDto);
        Task<ResponseModel<List<DepartamentoModel>>> EditarDepartamento(int id, DepartamentoEdicaoDto departamentoEdicaoDto);
        Task<ResponseModel<List<DepartamentoModel>>> RemoverDepartamento(int id);

    }
}
