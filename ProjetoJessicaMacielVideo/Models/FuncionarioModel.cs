using System.Text.Json.Serialization;

namespace ProjetoJessicaMacielVideo.Models
{
    public class FuncionarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public string RG { get; set; } = string.Empty;
        public int DepartamentoId { get; set; }
        [JsonIgnore]
        public DepartamentoModel Departamento { get; set; }
    }
}
