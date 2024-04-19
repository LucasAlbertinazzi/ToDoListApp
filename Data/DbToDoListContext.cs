using Microsoft.EntityFrameworkCore;

namespace AppToDoList.Data;

public partial class DbToDoListContext : DbContext
{
    public DbToDoListContext()
    {
    }

    public DbToDoListContext(DbContextOptions<DbToDoListContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblTarefa> TblTarefas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblTarefa>(entity =>
        {
            entity.HasKey(e => e.IdTarefas).HasName("tbl_tarefas_pkey");

            entity.ToTable("tbl_tarefas");

            entity.Property(e => e.IdTarefas).HasColumnName("idTarefas");
            entity.Property(e => e.DataConclusao)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dataConclusao");
            entity.Property(e => e.DataCriacao)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dataCriacao");
            entity.Property(e => e.Descricao)
                .HasColumnType("character varying")
                .HasColumnName("descricao");
            entity.Property(e => e.Status)
                .HasDefaultValue(false)
                .HasColumnName("status");
            entity.Property(e => e.Titulo)
                .HasColumnType("character varying")
                .HasColumnName("titulo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
