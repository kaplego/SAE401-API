using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idproduit), nameof(Idcouleur))]
[Table("t_j_coloration_col")]
public partial class Coloration
{
    [Key]
    [Column("col_idproduit")]
    public int Idproduit { get; set; }

    [Key]
    [Column("col_idcouleur")]
    public int Idcouleur { get; set; }

    [Column("col_prixvente", TypeName = "numeric(10, 2)")]
    [Range(0.0, double.MaxValue, ErrorMessage = "La valeur doit être supérieure ou égale à 0.")]
    public decimal Prixvente { get; set; }

    [Column("col_prixsolde", TypeName = "numeric(10, 2)")]
    [Range(0.0, double.MaxValue, ErrorMessage = "La valeur doit être supérieure ou égale à 0.")]
    public decimal? Prixsolde { get; set; }

    [Column("col_quantitestock")]
    [Range(0, int.MaxValue, ErrorMessage = "La valeur doit être supérieure ou égale à 0.")]
    public int Quantitestock { get; set; }

    [Column("col_descriptioncoloration")]
    [StringLength(2048)]
    public string? Descriptioncoloration { get; set; }

    [Column("col_estvisible")]
    public bool Estvisible { get; set; }

    [InverseProperty("Coloration")]
    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();

    [InverseProperty("Coloration")]
    public virtual ICollection<Detailcomposition> Detailcompositions { get; set; } = new List<Detailcomposition>();

    [InverseProperty("Coloration")]
    public virtual ICollection<Detailpanier> Detailpaniers { get; set; } = new List<Detailpanier>();

    [ForeignKey(nameof(Idcouleur))]
    [InverseProperty("Colorations")]
    public virtual Couleur IdcouleurNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idproduit))]
    [InverseProperty("Colorations")]
    public virtual Produit IdproduitNavigation { get; set; } = null!;

    [InverseProperty("Colorations")]
    public virtual ICollection<Photocoloration> Photocolorations { get; set; } = new List<Photocoloration>();

    [InverseProperty("Colorations")]
    public virtual ICollection<Detailregroupement> Detailregroupements { get; set; } = new List<Detailregroupement>();
}
