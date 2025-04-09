using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    [InverseProperty(nameof(Detailcommande.ColorationNavigation))]
    public virtual ICollection<Detailcommande> DetailsCommandeNavigation { get; set; } = new List<Detailcommande>();

    [InverseProperty(nameof(Detailcomposition.ColorationNavigation))]
    public virtual ICollection<Detailcomposition> DetailsCompositionNavigation { get; set; } = new List<Detailcomposition>();

    [InverseProperty(nameof(Detailpanier.ColorationNavigation))]
    public virtual ICollection<Detailpanier> DetailsPanierNavigation { get; set; } = new List<Detailpanier>();

    [ForeignKey(nameof(Idcouleur))]
    [InverseProperty(nameof(Couleur.ColorationsNavigation))]
    public virtual Couleur CouleurNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idproduit))]
    [InverseProperty(nameof(Produit.ColorationsNavigation))]
    public virtual Produit ProduitNavigation { get; set; } = null!;

    [InverseProperty(nameof(Photocoloration.ColorationNavigation))]
    public virtual ICollection<Photocoloration> PhotocolsNavigation { get; set; } = new List<Photocoloration>();

    [InverseProperty(nameof(Detailregroupement.ColorationsNavigation))]
    public virtual ICollection<Detailregroupement> DetailRegroupementNavigation { get; set; } = new List<Detailregroupement>();
}
