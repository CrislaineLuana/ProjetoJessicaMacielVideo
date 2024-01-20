using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoJessicaMacielVideo.Dto.FuncionarioDto;
using ProjetoJessicaMacielVideo.Models;
using ProjetoJessicaMacielVideo.Services.FuncionarServices;

namespace ProjetoJessicaMacielVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioInterface _funcionarioInterface;

        public FuncionarioController(IFuncionarioInterface funcionarioInterface)
        {
            _funcionarioInterface = funcionarioInterface;
        }


        [HttpGet("ListarFuncionarios")]
        public async Task<ActionResult<ResponseModel<List<FuncionarioModel>>>> ListarFuncionarios()
        {
            var funcionarios = await _funcionarioInterface.ListarFuncionarios();
            return Ok(funcionarios);
        }

        [HttpGet("ListarFuncionariosPorId/{id}")]
        public async Task<ActionResult<ResponseModel<FuncionarioModel>>> ListarFuncionariosPorId(int id)
        {
            var funcionario = await _funcionarioInterface.ListarFuncionariosPorId(id);
            return Ok(funcionario);
        }

        [HttpGet("ListarFuncionariosPorDepartamentoId/{idDepartamento}")]
        public async Task<ActionResult<ResponseModel<List<FuncionarioModel>>>> ListarFuncionariosPorDepartamentoId(int idDepartamento)
        {
            var funcionarios = await _funcionarioInterface.ListarFuncionariosPorDepartamentoId(idDepartamento);
            return Ok(funcionarios);
        }


        [HttpPost("CriarFuncionario")]
        public async Task<ActionResult<ResponseModel<List<FuncionarioModel>>>> CriarFuncionario([FromForm] FuncionarioCriacaoDto funcionarioCriacaoDto, [FromForm] ICollection<IFormFile> foto) 
        {
            var funcionarios = await _funcionarioInterface.CriarFuncionario(funcionarioCriacaoDto, foto);
            return Ok(funcionarios);
        }

        [HttpPut("EditarFuncionario/{id}")]
        public async Task<ActionResult<ResponseModel<List<FuncionarioModel>>>> EditarFuncionario(  int id, [FromForm] FuncionarioEdicaoDto funcionarioEdicaoDto, [FromForm] ICollection<IFormFile> foto)
        {
            var funcionarios = await _funcionarioInterface.EditarFuncionario(id, funcionarioEdicaoDto, foto);
            return Ok(funcionarios);
        }


        [HttpDelete("RemoverFuncionario/{id}")]
        public async Task<ActionResult<ResponseModel<List<FuncionarioModel>>>> RemoverFuncionario(int id)
        {
            var funcionarios = await _funcionarioInterface.RemoverFuncionario(id);
            return Ok(funcionarios);
        }

    }
}
