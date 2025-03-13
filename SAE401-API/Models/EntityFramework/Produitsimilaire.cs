using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idproduit), nameof(Idproduit2))]
[Table("t_j_produitsimilaire_pds")]
public partial class Produitsimilaire
{
    [Key]
    [Column("pds_idproduit")]
    public int Idproduit { get; set; }

    [Key]
    [Column("pds_idproduit2")]
    public int Idproduit2 { get; set; }

    [ForeignKey(nameof(Idproduit2))]
    [InverseProperty("Idproduitsimilaire")]
    public virtual Produit IdproduitNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idproduit))]
    [InverseProperty("Idproduitsimilaire2")]
    public virtual Produit IdproduitNavigation2 { get; set; } = null!;
}
