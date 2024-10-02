using System;
using System.Collections.Generic;
using api_sistema_agente.Domain.Entities;
using api_sistema_agente.Infrastructure.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace api_sistema_agente.Infrastructure.Config;

public partial class DatabaseContext : DbContext
{

    private readonly ISecretsService _secrets;

    public DatabaseContext(DbContextOptions<DatabaseContext> options, ISecretsService secrets) : base(options)
    {
        _secrets = secrets;
    }

    public virtual DbSet<Auth> Auth { get; set; }

    public virtual DbSet<Pendencie> Pendencias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_secrets.GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Auth__3213E83FF5027249");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LOGIN");
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MAIL");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
        });

        modelBuilder.Entity<Pendencie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pendenci__3213E83FF5027249");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AgentName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("AGENT_NAME");
            entity.Property(e => e.DateRegister)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("DATE_REGISTER");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Document)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("DOCUMENT");
            entity.Property(e => e.IdAgent).HasColumnName("ID_AGENT");
            entity.Property(e => e.IdProtocol)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ID_PROTOCOL");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PRODUCT_NAME");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
