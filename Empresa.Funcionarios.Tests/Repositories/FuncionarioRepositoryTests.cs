using Empresa.Funcionarios.Data;
using Empresa.Funcionarios.Data.Repositories;
using Empresa.Funcionarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Empresa.Funcionarios.Tests.Repositories;

public class FuncionarioRepositoryTests
{
    private FuncionarioDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<FuncionarioDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        return new FuncionarioDbContext(options);
    }

    [Fact]
    public async Task AddAsync_Deve_Adicionar_Funcionario()
    {
        var context = GetDbContext();
        var logger = new Mock<ILogger<FuncionarioRepository>>().Object;
        var repository = new FuncionarioRepository(context, logger);
        var funcionario = new Funcionario
        {
            Id = Guid.NewGuid(),
            Nome = "João",
            Sobrenome = "Silva",
            Email = "joao@email.com",
            NumeroDocumento = "12345678900",
            DataNascimento = new DateTime(2000, 1, 1)
        };

        await repository.AddAsync(funcionario);
        var result = await repository.GetByIdAsync(funcionario.Id);

        Assert.NotNull(result);
        Assert.Equal("João", result.Nome);
    }

    [Fact]
    public async Task GetAllAsync_Deve_Retornar_Todos_Os_Funcionarios()
    {
        var context = GetDbContext();
        var logger = new Mock<ILogger<FuncionarioRepository>>().Object;
        var repository = new FuncionarioRepository(context, logger);
        await repository.AddAsync(new Funcionario { Id = Guid.NewGuid(), Nome = "Carlos", NumeroDocumento = "123" });
        await repository.AddAsync(new Funcionario { Id = Guid.NewGuid(), Nome = "Ana", NumeroDocumento = "456" });

        var result = await repository.GetAllAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_Deve_Retornar_Funcionario_Se_Existir()
    {
        var context = GetDbContext();
        var logger = new Mock<ILogger<FuncionarioRepository>>().Object;
        var repository = new FuncionarioRepository(context, logger);
        var funcionario = new Funcionario { Id = Guid.NewGuid(), Nome = "Pedro", NumeroDocumento = "789" };
        await repository.AddAsync(funcionario);

        var result = await repository.GetByIdAsync(funcionario.Id);

        Assert.NotNull(result);
        Assert.Equal("Pedro", result.Nome);
    }

    [Fact]
    public async Task DeleteAsync_Deve_Remover_Funcionario()
    {
        var context = GetDbContext();
        var logger = new Mock<ILogger<FuncionarioRepository>>().Object;
        var repository = new FuncionarioRepository(context, logger);
        var funcionario = new Funcionario { Id = Guid.NewGuid(), Nome = "Lucas", NumeroDocumento = "999" };
        await repository.AddAsync(funcionario);

        await repository.DeleteAsync(funcionario.Id);
        var result = await repository.GetByIdAsync(funcionario.Id);

        Assert.Null(result);
    }

    [Fact]
    public async Task ExistsAsync_Deve_Retornar_True_Se_Documento_Existir()
    {
        var context = GetDbContext();
        var logger = new Mock<ILogger<FuncionarioRepository>>().Object;
        var repository = new FuncionarioRepository(context, logger);
        var funcionario = new Funcionario { Id = Guid.NewGuid(), Nome = "Mariana", NumeroDocumento = "11122233344" };
        await repository.AddAsync(funcionario);

        var exists = await repository.ExistsAsync(funcionario.NumeroDocumento);

        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_Deve_Retornar_False_Se_Documento_Nao_Existir()
    {
        var context = GetDbContext();
        var logger = new Mock<ILogger<FuncionarioRepository>>().Object;
        var repository = new FuncionarioRepository(context, logger);

        var exists = await repository.ExistsAsync("00000000000");

        Assert.False(exists);
    }
}
