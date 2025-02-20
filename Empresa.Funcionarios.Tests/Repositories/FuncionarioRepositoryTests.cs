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
            DataNascimento = new DateTime(2000, 1, 1),
            Telefone = new List<string> { "11999999999" },
            SenhaHash = "hashed_password_example"
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
        await repository.AddAsync(new Funcionario
        {
            Id = Guid.NewGuid(),
            Nome = "Carlos",
            Sobrenome = "Pereira",                   
            Email = "carlos@email.com",             
            NumeroDocumento = "123",
            DataNascimento = new DateTime(1993, 5, 15), 
            Telefone = new List<string> { "11988887777" }, 
            SenhaHash = "hashed_password_carlos"
        });

        await repository.AddAsync(new Funcionario
        {
            Id = Guid.NewGuid(),
            Nome = "Jose",
            Sobrenome = "Silvano",
            Email = "j.silvano@email.com",
            NumeroDocumento = "123",
            DataNascimento = new DateTime(1997, 8, 26),
            Telefone = new List<string> { "11988887777" },
            SenhaHash = "hashed_password_silvano"
        });

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

        var funcionarioId = Guid.NewGuid();
        var funcionario = new Funcionario
        {
            Id = funcionarioId,
            Nome = "Pedro",
            Sobrenome = "Almeida",                          
            Email = "pedro@email.com",                      
            NumeroDocumento = "789",
            DataNascimento = new DateTime(1992, 3, 10),     
            Telefone = new List<string> { "11966665555" }, 
            SenhaHash = "hashed_password_pedro"                              
        };

        await repository.AddAsync(funcionario);

        var result = await repository.GetByIdAsync(funcionarioId);

        Assert.NotNull(result);
        Assert.Equal(funcionarioId, result.Id);
    }

    [Fact]
    public async Task DeleteAsync_Deve_Remover_Funcionario()
    {

        var context = GetDbContext();
        var logger = new Mock<ILogger<FuncionarioRepository>>().Object;
        var repository = new FuncionarioRepository(context, logger);

        var funcionarioId = Guid.NewGuid();
        var funcionario = new Funcionario
        {
            Id = funcionarioId,
            Nome = "Lucas",
            Sobrenome = "Ferreira",                         
            Email = "lucas@email.com",                      
            NumeroDocumento = "999",
            DataNascimento = new DateTime(1995, 7, 22),     
            Telefone = new List<string> { "11955554444" }, 
            SenhaHash = "hashed_password_lucas"
        };

        await repository.AddAsync(funcionario);

        await repository.DeleteAsync(funcionarioId);
        var result = await repository.GetByIdAsync(funcionarioId);

        Assert.Null(result);
    }

    [Fact]
    public async Task ExistsAsync_Deve_Retornar_True_Se_Documento_Existir()
    {
        var context = GetDbContext();
        var logger = new Mock<ILogger<FuncionarioRepository>>().Object;
        var repository = new FuncionarioRepository(context, logger);

        var funcionario = new Funcionario
        {
            Id = Guid.NewGuid(),
            Nome = "Mariana",
            Sobrenome = "Oliveira",                          
            Email = "mariana@email.com",                     
            NumeroDocumento = "11122233344",
            DataNascimento = new DateTime(1993, 9, 5),       
            Telefone = new List<string> { "11944443333" },  
            SenhaHash = "hashed_password_mariana"
        };

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
