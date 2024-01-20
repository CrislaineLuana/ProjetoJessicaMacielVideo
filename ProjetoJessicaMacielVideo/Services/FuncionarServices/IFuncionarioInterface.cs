using Microsoft.AspNetCore.Mvc;
using ProjetoJessicaMacielVideo.Dto.FuncionarioDto;
using ProjetoJessicaMacielVideo.Models;

namespace ProjetoJessicaMacielVideo.Services.FuncionarServices
{
    public interface IFuncionarioInterface
    {
        Task<ResponseModel<List<FuncionarioModel>>> ListarFuncionarios();
        Task<ResponseModel<FuncionarioModel>> ListarFuncionariosPorId(int id);
        Task<ResponseModel<List<FuncionarioModel>>> ListarFuncionariosPorDepartamentoId(int idDepartamento);
        Task<ResponseModel<List<FuncionarioModel>>> CriarFuncionario(FuncionarioCriacaoDto funcionarioCriacaoDto, ICollection<IFormFile> foto);
        Task<ResponseModel<List<FuncionarioModel>>> RemoverFuncionario(int id);
        Task<ResponseModel<List<FuncionarioModel>>> EditarFuncionario(int id,FuncionarioEdicaoDto funcionarioEdicaoDto,  ICollection<IFormFile> foto);
    }
}
