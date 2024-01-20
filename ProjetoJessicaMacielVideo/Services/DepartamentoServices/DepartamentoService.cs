using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoJessicaMacielVideo.Data;
using ProjetoJessicaMacielVideo.Dto.DepartamentoDto;
using ProjetoJessicaMacielVideo.Models;
using System.Collections.Generic;

namespace ProjetoJessicaMacielVideo.Services.DepartamentoServices
{
    public class DepartamentoService : IDepartamentoInterface
    {
        private readonly AppDbContext _context;

        public DepartamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<DepartamentoModel>>> CriarDepartamento(DepartamentoCriacaoDto departamentoCriacaoDto)
        {
            ResponseModel<List<DepartamentoModel>> resposta = new ResponseModel<List<DepartamentoModel>>();
            try
            {
                if(departamentoCriacaoDto == null)
                {
                    resposta.Mensagem = "Insira os dados para criação do departamento!";
                    resposta.Status = false;
                }

                var departamento = new DepartamentoModel()
                {
                    Nome = departamentoCriacaoDto.Nome,
                    Sigla = departamentoCriacaoDto.Sigla 
                };

                _context.Add(departamento);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Departamentos.ToListAsync();

                return resposta;



            }catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<DepartamentoModel>>> EditarDepartamento(int id, DepartamentoEdicaoDto departamentoEdicaoDto)
        {
            ResponseModel<List<DepartamentoModel>> resposta = new ResponseModel<List<DepartamentoModel>>();
            try
            {
                var departamento = await _context.Departamentos.FirstOrDefaultAsync(dep => dep.Id == id);

                if(departamento == null)
                {
                    resposta.Mensagem = "Não existe nenhum departamento com o id repassado!";
                    resposta.Status = false;
                    return resposta;
                }


                departamento.Nome = departamentoEdicaoDto.Nome;
                departamento.Sigla = departamentoEdicaoDto.Sigla;

                _context.Update(departamento);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Departamentos.ToListAsync();

                return resposta;


            }catch  (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<DepartamentoModel>> ListarDepartamentoPorId(int id)
        {
            ResponseModel<DepartamentoModel> resposta = new ResponseModel<DepartamentoModel>();
            try
            {
                var departamento = await _context.Departamentos.FirstOrDefaultAsync(dep => dep.Id == id);

                if(departamento == null)
                {
                    resposta.Mensagem = "Insira um id de departamento válido!";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = departamento;

                return resposta;


            }catch  (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<List<DepartamentoModel>>> ListarDepartamentos()
        {
            ResponseModel<List<DepartamentoModel>> resposta = new ResponseModel<List<DepartamentoModel>>();
            try
            {
                var departamentos = await _context.Departamentos.ToListAsync();

                if(departamentos.Count() == 0)
                {
                    resposta.Mensagem = "Não possui nenhum departamento cadastrado!";
                    return resposta;
                }

                resposta.Dados = departamentos;

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<DepartamentoModel>>> RemoverDepartamento(int id)
        {
            ResponseModel<List<DepartamentoModel>> resposta = new ResponseModel<List<DepartamentoModel>>();
            try
            {
                var departamento = await _context.Departamentos.FirstOrDefaultAsync(dep => dep.Id == id);

                if(departamento == null)
                {
                    resposta.Mensagem = "Nenhum departamento foi localizado com esse id!";
                    resposta.Status = false;
                    return resposta;
                }

                _context.Remove(departamento);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Departamentos.ToListAsync();

                return resposta;


            }
            catch(Exception ex) 
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }
    }
}
