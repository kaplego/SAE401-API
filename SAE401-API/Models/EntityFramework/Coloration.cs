using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey("Idproduit", "Idcouleur")]
[Table("coloration")]
[Index("Idproduit", "Idcouleur", Name = "coloration_pk", IsUnique = true)]
[Index("Idcouleur", Name = "colorationcouleur_fk")]
[Index("Idproduit", Name = "colorationproduit_fk")]
public partial class Coloration
{
    [Key]
    [Column("idproduit")]
    public int Idproduit { get; set; }

    [Key]
    [Column("idcouleur")]
    public int Idcouleur { get; set; }

    [Column("prixvente")]
    [Precision(10, 2)]
    public decimal Prixvente { get; set; }

    [Column("prixsolde")]
    [Precision(10, 2)]
    public decimal? Prixsolde { get; set; }

    [Column("quantitestock")]
    public int Quantitestock { get; set; }

    [Column("descriptioncoloration")]
    [StringLength(2048)]
    public string? Descriptioncoloration { get; set; }

    [Column("estvisible")]
    public bool Estvisible { get; set; }

    [InverseProperty("Coloration")]
    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();

    [InverseProperty("Coloration")]
    public virtual ICollection<Detailcomposition> Detailcompositions { get; set; } = new List<Detailcomposition>();

    [InverseProperty("Coloration")]
    public virtual ICollection<Detailpanier> Detailpaniers { get; set; } = new List<Detailpanier>();

    [ForeignKey("Idcouleur")]
    [InverseProperty("Colorations")]
    public virtual Couleur IdcouleurNavigation { get; set; } = null!;

    [ForeignKey("Idproduit")]
    [InverseProperty("Colorations")]
    public virtual Produit IdproduitNavigation { get; set; } = null!;

    [ForeignKey("Idproduit, Idcouleur")]
    [InverseProperty("Colorations")]
    public virtual ICollection<Photo> Idphotos { get; set; } = new List<Photo>();

    [ForeignKey("Idproduit, Idcouleur")]
    [InverseProperty("Colorations")]
    public virtual ICollection<Regroupementproduit> Idregroupements { get; set; } = new List<Regroupementproduit>();
}
