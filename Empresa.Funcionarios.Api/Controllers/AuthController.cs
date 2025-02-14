using Empresa.Funcionarios.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Empresa.Funcionarios.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Email == "admin@email.com" && request.Password == "123456") 
        {
            var token = _tokenService.GenerateToken(Guid.NewGuid().ToString(), request.Email);
            return Ok(new { Token = token });
        }

        return Unauthorized("Usuário ou senha inválidos.");
    }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
