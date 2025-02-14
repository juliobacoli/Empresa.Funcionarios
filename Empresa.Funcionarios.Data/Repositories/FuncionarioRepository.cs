using Empresa.Funcionarios.Domain.Entities;
using Empresa.Funcionarios.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Funcionarios.Data.Repositories;

public class FuncionarioRepository : IFuncionarioRepository
{
    private readonly FuncionarioDbContext _context;

    public FuncionarioRepository(FuncionarioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Funcionario>> GetAllAsync()
    {
        return await _context.Funcionarios.ToListAsync();
    }

    public async Task<Funcionario?> GetByIdAsync(Guid id)
    {
        return await _context.Funcionarios.FindAsync(id);
    }

    public async Task AddAsync(Funcionario funcionario)
    {
        _context.Funcionarios.Add(funcionario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Funcionario funcionario)
    {
        _context.Funcionarios.Update(funcionario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var funcionario = await GetByIdAsync(id);
        if (funcionario != null)
        {
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(string numeroDocumento)
    {
        return await _context.Funcionarios.AnyAsync(f => f.NumeroDocumento == numeroDocumento);
    }
}
