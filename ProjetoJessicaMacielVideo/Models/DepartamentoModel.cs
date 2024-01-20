namespace ProjetoJessicaMacielVideo.Models
{
    public class DepartamentoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
        public List<FuncionarioModel>? Funcionarios { get; set; }
    }
}
