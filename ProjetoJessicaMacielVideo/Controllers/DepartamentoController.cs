using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoJessicaMacielVideo.Dto.DepartamentoDto;
using ProjetoJessicaMacielVideo.Models;
using ProjetoJessicaMacielVideo.Services.DepartamentoServices;

namespace ProjetoJessicaMacielVideo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoInterface _departamentoInterface;

        public DepartamentoController(IDepartamentoInterface departamentoInterface)
        {
            _departamentoInterface = departamentoInterface;
        }

        [HttpGet("ListarDepartamentos")]
        public async Task<ActionResult<ResponseModel<List<DepartamentoModel>>>> ListarDepartamentos()
        {
            var departamentos = await _departamentoInterface.ListarDepartamentos();
            return Ok(departamentos);
        }


        [HttpGet("ListarDepartamentoPorId/{id}")]
        public async Task<ActionResult<ResponseModel<DepartamentoModel>>> ListarDepartamentoPorId(int id)
        {
            var departamento = await _departamentoInterface.ListarDepartamentoPorId(id);
            return Ok(departamento);
        }


        [HttpPost("CriarDepartamento")]
        public async Task<ActionResult<ResponseModel<List<DepartamentoModel>>>> CriarDepartamento(DepartamentoCriacaoDto departamentoCriacaoDto)
        {
            var departamentos = await _departamentoInterface.CriarDepartamento(departamentoCriacaoDto);
            return Ok(departamentos);
        }


        [HttpPut("EditarDepartamento/{id}")]
        public async Task<ActionResult<ResponseModel<List<DepartamentoModel>>>> EditarDepartamento(int id, DepartamentoEdicaoDto departamentoEdicaoDto)
        {
            var departamentos = await _departamentoInterface.EditarDepartamento(id, departamentoEdicaoDto);
            return Ok(departamentos);
        }


        [HttpDelete("RemoverDepartamento/{id}")]
        public async Task<ActionResult<ResponseModel<List<DepartamentoModel>>>> RemoverDepartamento(int id)
        {
            var departamentos = await _departamentoInterface.RemoverDepartamento(id);
            return Ok(departamentos);
        }

    }
}
