using System;
using System.Collections.Generic;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=database-1-app-to-do-list.cv2aa8q4sz5s.sa-east-1.rds.amazonaws.com;Port=5432;DataBase=dbToDoList;Username=postgres;Password=postgres;");

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
