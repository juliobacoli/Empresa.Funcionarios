using Empresa.Funcionarios.Domain.Entities;
using Empresa.Funcionarios.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Empresa.Funcionarios.Data.Repositories;

public class FuncionarioRepository : IFuncionarioRepository
{
    private readonly FuncionarioDbContext _context;
    private readonly ILogger<FuncionarioRepository> _logger;

    public FuncionarioRepository(FuncionarioDbContext context, ILogger<FuncionarioRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Funcionario>> GetAllAsync()
    {
        _logger.LogInformation("Buscando todos os funcionários no banco de dados.");
        var funcionarios = await _context.Funcionarios.ToListAsync();
        _logger.LogInformation("Total de funcionários encontrados: {Total}", funcionarios.Count);
        return funcionarios;
    }

    public async Task<Funcionario?> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("Buscando funcionário por ID no banco: {FuncionarioId}", id);
        var funcionario = await _context.Funcionarios.FindAsync(id);

        if (funcionario == null)
            _logger.LogWarning("Funcionário não encontrado no banco: {FuncionarioId}", id);

        return funcionario;
    }

    public async Task AddAsync(Funcionario funcionario)
    {
        _logger.LogInformation("Adicionando novo funcionário no banco: {@Funcionario}", funcionario);
        _context.Funcionarios.Add(funcionario);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Funcionário adicionado com sucesso: {FuncionarioId}", funcionario.Id);
    }

    public async Task UpdateAsync(Funcionario funcionario)
    {
        _logger.LogInformation("Atualizando funcionário no banco: {FuncionarioId}", funcionario.Id);
        _context.Funcionarios.Update(funcionario);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Funcionário atualizado com sucesso: {FuncionarioId}", funcionario.Id);
    }

    public async Task DeleteAsync(Guid id)
    {
        _logger.LogInformation("Removendo funcionário do banco: {FuncionarioId}", id);
        var funcionario = await GetByIdAsync(id);
        if (funcionario != null)
        {
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Funcionário removido com sucesso: {FuncionarioId}", id);
        }
    }

    public async Task<bool> ExistsAsync(string numeroDocumento)
    {
        _logger.LogInformation("Verificando existência de funcionário com documento: {NumeroDocumento}", numeroDocumento);
        var exists = await _context.Funcionarios.AnyAsync(f => f.NumeroDocumento == numeroDocumento);
        _logger.LogInformation("Resultado da verificação: {Existe}", exists);
        return exists;
    }
}
