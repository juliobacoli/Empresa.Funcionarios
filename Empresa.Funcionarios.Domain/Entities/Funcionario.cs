using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empresa.Funcionarios.Domain.Entities;

public class Funcionario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    public string Sobrenome { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string NumeroDocumento { get; set; }  

    public List<string> Telefone { get; set; }

    public Guid? GestorId { get; set; } 

    public DateTime DataNascimento { get; set; }

    public string SenhaHash { get; set; } 
}
