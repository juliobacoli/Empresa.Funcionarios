using Empresa.Funcionarios.Domain.Entities;
using Empresa.Funcionarios.Domain.Repositories;

namespace Empresa.Funcionarios.Application.Services;

public class FuncionarioService : IFuncionarioService
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public FuncionarioService(IFuncionarioRepository funcionarioRepository)
    {
        _funcionarioRepository = funcionarioRepository;
    }

    public async Task<IEnumerable<Funcionario>> GetAllAsync()
    {
        return await _funcionarioRepository.GetAllAsync();
    }

    public async Task<Funcionario?> GetByIdAsync(Guid id)
    {
        return await _funcionarioRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Funcionario funcionario)
    {
        //Não permitir duplicidade de documento
        if (await _funcionarioRepository.ExistsAsync(funcionario.NumeroDocumento))
            throw new Exception("Já existe um funcionário com este documento.");

        //Funcionário deve ser maior de idade
        if ((DateTime.UtcNow - funcionario.DataNascimento).TotalDays / 365 < 18)
            throw new Exception("O funcionário deve ter no mínimo 18 anos.");

        await _funcionarioRepository.AddAsync(funcionario);
    }

    public async Task UpdateAsync(Funcionario funcionario)
    {
        var existingFuncionario = await _funcionarioRepository.GetByIdAsync(funcionario.Id);
        if (existingFuncionario == null)
            throw new Exception("Funcionário não encontrado.");

        //Não permitir alteração para um documento já existente
        if (existingFuncionario.NumeroDocumento != funcionario.NumeroDocumento &&
            await _funcionarioRepository.ExistsAsync(funcionario.NumeroDocumento))
            throw new Exception("Já existe um funcionário com este documento.");

        await _funcionarioRepository.UpdateAsync(funcionario);
    }

    public async Task DeleteAsync(Guid id)
    {
        var funcionario = await _funcionarioRepository.GetByIdAsync(id);
        if (funcionario == null)
            throw new Exception("Funcionário não encontrado.");

        await _funcionarioRepository.DeleteAsync(id);
    }
}
