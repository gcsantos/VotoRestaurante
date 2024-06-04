using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotoRestaurante.Models;

public class Voto
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;
    [Required]
    [StringLength(11)]
    public string Cpf { get; set; } = string.Empty;
    [Required]
    public int RestauranteId { get; set; }
    public DateTime DataRegistro  { get; set; } = DateTime.Now;


    //[ForeignKey("RestauranteId")]
    //public Restaurante Restaurante { get; set; } = default!;
}

public class VotoLista
{
    public int Id { get; set; }

}

public class VotoCampeao
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Total { get; set; }

}
