

using Empresa.Funcionarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Funcionarios.Data;

public class FuncionarioDbContext : DbContext
{
    public FuncionarioDbContext(DbContextOptions<FuncionarioDbContext> options) : base(options) { }
    public DbSet<Funcionario> Funcionarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Funcionario>()
            .HasIndex(f => f.NumeroDocumento)
            .IsUnique();
    }
}
