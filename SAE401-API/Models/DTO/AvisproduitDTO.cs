using System.ComponentModel.DataAnnotations;

namespace SAE401_API.Models.DTO;

public partial class AvisproduitDTO
{
    public int? Idavis { get; set; }

    [Required]
    public int Idproduit { get; set; }

    [Required]
    public int Idclient { get; set; }

    [Required]
    public int Noteavis { get; set; }

    [Required]
    public DateTime Dateavis { get; set; } = DateTime.UtcNow;

    public string? Commentaireavis { get; set; }

    public string? Reponsemiliboo { get; set; }

}
