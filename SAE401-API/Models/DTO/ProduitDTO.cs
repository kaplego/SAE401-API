using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

public partial class ProduitDTO
{
    [Required]
    public int Idproduit { get; set; }

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
