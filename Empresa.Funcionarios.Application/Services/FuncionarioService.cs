using Empresa.Funcionarios.Domain.Entities;
using Empresa.Funcionarios.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Empresa.Funcionarios.Application.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly ILogger<FuncionarioService> _logger;

    public FuncionarioService(IFuncionarioRepository funcionarioRepository, ILogger<FuncionarioService> logger)
    {
        _funcionarioRepository = funcionarioRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Funcionario>> GetAllAsync()
    {
        _logger.LogInformation("Obtendo todos os funcionários.");
        var funcionarios = await _funcionarioRepository.GetAllAsync();
        _logger.LogInformation("Total de funcionários obtidos: {Total}", funcionarios.Count());
        return funcionarios;
    }

    public async Task<Funcionario?> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando funcionário por ID: {FuncionarioId}", id);
        var funcionario = await _funcionarioRepository.GetByIdAsync(id);

        if (funcionario == null)
            _logger.LogWarning("Funcionário não encontrado: {FuncionarioId}", id);

        return funcionario;
    }

    public async Task AddAsync(Funcionario funcionario)
    {
        _logger.LogInformation("Tentando adicionar novo funcionário: {@Funcionario}", funcionario);

        if (await _funcionarioRepository.ExistsAsync(funcionario.NumeroDocumento))
        {
            _logger.LogWarning("Tentativa de criação falhou. Documento já existente: {NumeroDocumento}", funcionario.NumeroDocumento);
            throw new Exception("Já existe um funcionário com este documento.");
        }

        if ((DateTime.UtcNow - funcionario.DataNascimento).TotalDays / 365 < 18)
        {
            _logger.LogWarning("Tentativa de criação falhou. Funcionário menor de idade: {@Funcionario}", funcionario);
            throw new Exception("O funcionário deve ter no mínimo 18 anos.");
        }

        await _funcionarioRepository.AddAsync(funcionario);
        _logger.LogInformation("Funcionário criado com sucesso: {FuncionarioId}", funcionario.Id);

    }

    public async Task UpdateAsync(Funcionario funcionario)
    {
        _logger.LogInformation("Tentando atualizar funcionário: {FuncionarioId}", funcionario.Id);

        var existingFuncionario = await _funcionarioRepository.GetByIdAsync(funcionario.Id);
        if (existingFuncionario == null)
        {
            _logger.LogWarning("Tentativa de atualização falhou. Funcionário não encontrado: {FuncionarioId}", funcionario.Id);
            throw new Exception("Funcionário não encontrado.");
        }

        if (existingFuncionario.NumeroDocumento != funcionario.NumeroDocumento &&
            await _funcionarioRepository.ExistsAsync(funcionario.NumeroDocumento))
        {
            _logger.LogWarning("Tentativa de atualização falhou. Documento já existente: {NumeroDocumento}", funcionario.NumeroDocumento);
            throw new Exception("Já existe um funcionário com este documento.");
        }

        await _funcionarioRepository.UpdateAsync(funcionario);
        _logger.LogInformation("Funcionário atualizado com sucesso: {FuncionarioId}", funcionario.Id);

    }

    public async Task DeleteAsync(Guid id)
    {
        _logger.LogInformation("Tentando excluir funcionário: {FuncionarioId}", id);

        var funcionario = await _funcionarioRepository.GetByIdAsync(id);
        if (funcionario == null)
        {
            _logger.LogWarning("Tentativa de exclusão falhou. Funcionário não encontrado: {FuncionarioId}", id);
            throw new Exception("Funcionário não encontrado.");
        }

        await _funcionarioRepository.DeleteAsync(id);
        _logger.LogInformation("Funcionário excluído com sucesso: {FuncionarioId}", id);
    }
}
