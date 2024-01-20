using Microsoft.EntityFrameworkCore;
using ProjetoJessicaMacielVideo.Data;
using ProjetoJessicaMacielVideo.Dto.FuncionarioDto;
using ProjetoJessicaMacielVideo.Models;
using System.Collections.Generic;

namespace ProjetoJessicaMacielVideo.Services.FuncionarServices
{
    public class FuncionarioService : IFuncionarioInterface
    {
        private readonly AppDbContext _context;

        public FuncionarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<FuncionarioModel>>> CriarFuncionario(FuncionarioCriacaoDto funcionarioCriacaoDto, ICollection<IFormFile> foto)
        {
            ResponseModel<List<FuncionarioModel>> resposta = new ResponseModel<List<FuncionarioModel>>();
            try
            {

                if(funcionarioCriacaoDto == null)
                {
                    resposta.Mensagem = "Informe os dados para criações de um funcionário!";
                    resposta.Status = false;
                    return resposta;
                }

                var departamento = await _context.Departamentos.FirstOrDefaultAsync(dep => dep.Id == funcionarioCriacaoDto.DepartamentoId);

                if(departamento == null)
                {
                    resposta.Mensagem = "Departamento inserido não localizado!";
                    resposta.Status = false;
                    return resposta;
                }

                var caminhoFoto = "";

                if(foto.Count() > 0)
                {
                    caminhoFoto = GerarCaminhoFoto(foto.First());
                }

                var funcionario = new FuncionarioModel()
                {
                    Nome = funcionarioCriacaoDto.Nome,
                    RG = funcionarioCriacaoDto.RG,
                    DepartamentoId = funcionarioCriacaoDto.DepartamentoId,
                    Foto = foto.Count() > 0 ? caminhoFoto : null,
                    Departamento = departamento
                };

                _context.Add(funcionario);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Funcionarios.ToListAsync();

                return resposta;


            }catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<FuncionarioModel>>> EditarFuncionario(int id, FuncionarioEdicaoDto funcionarioEdicaoDto, ICollection<IFormFile> foto)
        {
            ResponseModel<List<FuncionarioModel>> resposta = new ResponseModel<List<FuncionarioModel>>();
            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(func => func.Id == id);

                if ( funcionario == null)
                {
                    resposta.Mensagem = "Nenhum funcionário localizado com esse Id!";
                    resposta.Status = false;
                    return resposta;
                }


                var caminhoImagem = "";
                if(foto.Count() > 0)
                {
                    if(funcionario.Foto != null)
                    {
                        var caminhoImagemExistente = "Imagem\\" + funcionario.Foto;

                        if (File.Exists(caminhoImagemExistente))
                        {
                            File.Delete(caminhoImagemExistente);
                        }
                    }

                    caminhoImagem = GerarCaminhoFoto(foto.First());
                }


                var departamento = await _context.Departamentos.FirstOrDefaultAsync(dep => dep.Id == funcionarioEdicaoDto.DepartamentoId);

                if(departamento == null)
                {
                    resposta.Mensagem = "Digite um id válido para o departamento!";
                    resposta.Status = false;
                    return resposta;
                }

                funcionario.Nome = funcionarioEdicaoDto.Nome;
                funcionario.RG = funcionarioEdicaoDto.RG;
                funcionario.DepartamentoId = funcionarioEdicaoDto.DepartamentoId;
                funcionario.Departamento = funcionarioEdicaoDto.DepartamentoId == 0 ? funcionario.Departamento : departamento;
                funcionario.Foto = caminhoImagem == "" ? funcionario.Foto : caminhoImagem;

                _context.Update(funcionario);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Funcionarios.ToListAsync();

                return resposta;


            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public string GerarCaminhoFoto(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeCaminhoArquivo = foto.FileName.ToLower() + codigoUnico + ".png";

            var caminhoSalvarImagem = "Imagem\\";

            if (!Directory.Exists(caminhoSalvarImagem))
            {
                Directory.CreateDirectory(caminhoSalvarImagem);
            }

            using(var stream = File.Create(caminhoSalvarImagem + nomeCaminhoArquivo))
            {
                foto.CopyToAsync(stream).Wait();
            }

            return nomeCaminhoArquivo;

        }



        public async Task<ResponseModel<List<FuncionarioModel>>> ListarFuncionarios()
        {
            ResponseModel<List<FuncionarioModel>> resposta = new ResponseModel<List<FuncionarioModel>>();
            try
            {
                var funcionarios = await _context.Funcionarios.ToListAsync();

                if(funcionarios.Count() == 0)
                {
                    resposta.Mensagem = "Não existe funcionário cadastrado!";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = funcionarios;

                return resposta;



            }catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<FuncionarioModel>>> ListarFuncionariosPorDepartamentoId(int idDepartamento)
        {
            ResponseModel<List<FuncionarioModel>> resposta = new ResponseModel<List<FuncionarioModel>>();
            try
            {
                var departamento = await _context.Departamentos.FirstOrDefaultAsync(dep => dep.Id == idDepartamento);

                if(departamento == null)
                {
                    resposta.Mensagem = "Nenhum departamento foi localizado com esse id!";
                    resposta.Status = false;
                    return resposta;
                }

                var funcionarios = await _context.Funcionarios.Where(func => func.DepartamentoId == idDepartamento).ToListAsync();

                resposta.Dados = funcionarios;

                return resposta;


            }catch  (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<FuncionarioModel>> ListarFuncionariosPorId(int id)
        {
            ResponseModel<FuncionarioModel> resposta = new ResponseModel<FuncionarioModel>();
            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(func => func.Id == id);

                if(funcionario == null)
                {
                    resposta.Mensagem = "Nenhum funcionário localizado com esse id!";
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = funcionario;

                return resposta;


            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<List<FuncionarioModel>>> RemoverFuncionario(int id)
        {
            ResponseModel<List<FuncionarioModel>> resposta = new ResponseModel<List<FuncionarioModel>>();
            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(func => func.Id == id);

                if(funcionario == null)
                {
                    resposta.Mensagem = "Nenhum funcionário localizado com o id repassado!";
                    resposta.Status = false;
                    return resposta;
                }

                _context.Remove(funcionario);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Funcionarios.ToListAsync();

                return resposta;



            }catch(Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
