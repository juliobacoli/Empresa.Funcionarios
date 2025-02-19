using Empresa.Funcionarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Funcionarios.Data;

public class FuncionarioDbContext : DbContext
{
    public FuncionarioDbContext() { }

    public FuncionarioDbContext(DbContextOptions<FuncionarioDbContext> options) : base(options) { }
    public DbSet<Funcionario> Funcionarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=EmpresaFuncionarios;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Funcionario>()
            .HasIndex(f => f.NumeroDocumento)
            .IsUnique();
    }
}
