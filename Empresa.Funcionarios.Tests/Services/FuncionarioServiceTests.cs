using Empresa.Funcionarios.Application.Services;
using Empresa.Funcionarios.Domain.Entities;
using Empresa.Funcionarios.Domain.Repositories;
using Moq;

namespace Empresa.Funcionarios.Tests.Services;

public class FuncionarioServiceTests
{
    private readonly FuncionarioService _funcionarioService;
    private readonly Mock<IFuncionarioRepository> _funcionarioRepositoryMock;

    public FuncionarioServiceTests()
    {
        _funcionarioRepositoryMock = new Mock<IFuncionarioRepository>();
        _funcionarioService = new FuncionarioService(_funcionarioRepositoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_Deve_Lancar_Exception_Se_Documento_Existir()
    {
        var funcionario = new Funcionario
        {
            Id = Guid.NewGuid(),
            Nome = "João",
            Sobrenome = "Silva",
            Email = "joao@email.com",
            NumeroDocumento = "12345678900",
            DataNascimento = new DateTime(2000, 1, 1)
        };

        _funcionarioRepositoryMock
            .Setup(repo => repo.ExistsAsync(funcionario.NumeroDocumento))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<Exception>(() => _funcionarioService.AddAsync(funcionario));
    }

    [Fact]
    public async Task AddAsync_Deve_Criar_Funcionario_Se_Documento_For_Valido()
    {
        var funcionario = new Funcionario
        {
            Id = Guid.NewGuid(),
            Nome = "Maria",
            Sobrenome = "Silva",
            Email = "maria@email.com",
            NumeroDocumento = "98765432100",
            DataNascimento = new DateTime(2000, 1, 1)
        };

        _funcionarioRepositoryMock
            .Setup(repo => repo.ExistsAsync(funcionario.NumeroDocumento))
            .ReturnsAsync(false);

        await _funcionarioService.AddAsync(funcionario);

        _funcionarioRepositoryMock.Verify(repo => repo.AddAsync(funcionario), Times.Once);
    }

    [Fact]
    public async Task AddAsync_Deve_Lancar_Exception_Se_Funcionario_For_Menor_De_18_Anos()
    {
        var funcionario = new Funcionario
        {
            Id = Guid.NewGuid(),
            Nome = "Lucas",
            Sobrenome = "Souza",
            Email = "lucas@email.com",
            NumeroDocumento = "11122233344",
            DataNascimento = DateTime.UtcNow.AddYears(-17) // Menor de 18 anos
        };

        await Assert.ThrowsAsync<Exception>(() => _funcionarioService.AddAsync(funcionario));
    }

    [Fact]
    public async Task GetAllAsync_Deve_Retornar_Lista_De_Funcionarios()
    {
        var funcionarios = new List<Funcionario>
        {
            new Funcionario { Id = Guid.NewGuid(), Nome = "Carlos", NumeroDocumento = "123" },
            new Funcionario { Id = Guid.NewGuid(), Nome = "Ana", NumeroDocumento = "456" }
        };

        _funcionarioRepositoryMock
            .Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(funcionarios);

        var result = await _funcionarioService.GetAllAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_Deve_Retornar_Funcionario_Se_Existir()
    {
        var funcionarioId = Guid.NewGuid();
        var funcionario = new Funcionario { Id = funcionarioId, Nome = "Pedro", NumeroDocumento = "789" };

        _funcionarioRepositoryMock
            .Setup(repo => repo.GetByIdAsync(funcionarioId))
            .ReturnsAsync(funcionario);

        var result = await _funcionarioService.GetByIdAsync(funcionarioId);

        Assert.NotNull(result);
        Assert.Equal("Pedro", result.Nome);
    }

    [Fact]
    public async Task DeleteAsync_Deve_Lancar_Exception_Se_Funcionario_Nao_Existir()
    {
        var funcionarioId = Guid.NewGuid();

        _funcionarioRepositoryMock
            .Setup(repo => repo.GetByIdAsync(funcionarioId))
            .ReturnsAsync((Funcionario)null);

        await Assert.ThrowsAsync<Exception>(() => _funcionarioService.DeleteAsync(funcionarioId));
    }
}
