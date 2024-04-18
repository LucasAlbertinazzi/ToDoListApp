using AppToDoList.Models;
using Microsoft.EntityFrameworkCore;

namespace AppToDoList.Data
{
    public class ToListDbContext : DbContext
    {
        public ToListDbContext(DbContextOptions<ToListDbContext> options) : base(options)
        {
        }

        public DbSet<TarefaInfo> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações adicionais do modelo, se necessário
        }
    }
}
