namespace ProjetoJessicaMacielVideo.Dto.FuncionarioDto
{
    public class FuncionarioCriacaoDto
    {
        public string Nome { get; set; } = string.Empty;
        public string RG { get; set; } = string.Empty;
        public int DepartamentoId { get; set; }
    }
}
