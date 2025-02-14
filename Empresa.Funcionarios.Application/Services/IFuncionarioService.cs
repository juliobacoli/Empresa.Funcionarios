using Empresa.Funcionarios.Domain.Entities;

namespace Empresa.Funcionarios.Application.Services;

public interface IFuncionarioService
{
    Task<IEnumerable<Funcionario>> GetAllAsync();
    Task<Funcionario?> GetByIdAsync(Guid id);
    Task AddAsync(Funcionario funcionario);
    Task UpdateAsync(Funcionario funcionario);
    Task DeleteAsync(Guid id);
}