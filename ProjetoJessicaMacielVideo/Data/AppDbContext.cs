using Microsoft.EntityFrameworkCore;
using ProjetoJessicaMacielVideo.Models;

namespace ProjetoJessicaMacielVideo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {          
        }

        public DbSet<DepartamentoModel> Departamentos { get; set; }
        public DbSet<FuncionarioModel> Funcionarios { get; set; }

    }
}
