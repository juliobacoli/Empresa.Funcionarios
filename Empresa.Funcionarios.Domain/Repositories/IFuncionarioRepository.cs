using Empresa.Funcionarios.Domain.Entities;

namespace Empresa.Funcionarios.Domain.Repositories;

public interface IFuncionarioRepository
{
    Task<IEnumerable<Funcionario>> GetAllAsync();
    Task<Funcionario?> GetByIdAsync(Guid id);
    Task AddAsync(Funcionario funcionario);
    Task UpdateAsync(Funcionario funcionario);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(string numeroDocumento);
}
