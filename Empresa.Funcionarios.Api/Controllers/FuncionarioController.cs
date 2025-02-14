using Empresa.Funcionarios.Application.Services;
using Empresa.Funcionarios.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Empresa.Funcionarios.Api.Controllers;

[ApiController]
[Route("api/funcionarios")]
public class FuncionarioController : ControllerBase
{
    private readonly IFuncionarioService _funcionarioService;

    public FuncionarioController(IFuncionarioService funcionarioService)
    {
        _funcionarioService = funcionarioService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var funcionarios = await _funcionarioService.GetAllAsync();
        return Ok(funcionarios);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var funcionario = await _funcionarioService.GetByIdAsync(id);

        if (funcionario == null)
            return NotFound("Funcionário não encontrado.");

        return Ok(funcionario);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Funcionario funcionario)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _funcionarioService.AddAsync(funcionario);
            return CreatedAtAction(nameof(GetById), new { id = funcionario.Id }, funcionario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Funcionario funcionario)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != funcionario.Id)
            return BadRequest("ID do funcionário não corresponde.");

        try
        {
            await _funcionarioService.UpdateAsync(funcionario);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _funcionarioService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
