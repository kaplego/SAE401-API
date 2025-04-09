using System.ComponentModel.DataAnnotations;

namespace SAE401_API.Models.DTO;

public partial class ProduitDTO
{
    public int? Idproduit { get; set; }

    [Required]
    public int Idtypeproduit { get; set; }

    [Required]
    public int Idpays { get; set; }

    [Required]
    public string Nomproduit { get; set; } = null!;

    public string? Sourcenotice { get; set; }

    public string? Sourceaspecttechnique { get; set; }

    [Required]
    public int Delailivraison { get; set; }

    [Required]
    public decimal Coutlivraison { get; set; }

    [Required]
    public int Nbpaiementmax { get; set; }
}
