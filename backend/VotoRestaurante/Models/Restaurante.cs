using System.ComponentModel.DataAnnotations;

namespace VotoRestaurante.Models;

public class Restaurante
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)] 
    public string Nome { get; set; } = string.Empty;
    [Required]
    public bool Participa { get; set; }
}
