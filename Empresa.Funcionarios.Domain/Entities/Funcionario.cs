using System.ComponentModel.DataAnnotations;

namespace Empresa.Funcionarios.Domain.Entities;

public class Funcionario
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Nome { get; set; }

    [Required]
    public string Sobrenome { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string NumeroDocumento { get; set; }  

    public List<string> Telefones { get; set; } = new List<string>();

    public Guid? GestorId { get; set; } 

    public DateTime DataNascimento { get; set; }

    public string SenhaHash { get; set; } 
}
