using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(IdproduitSim), nameof(IdproduitRef))]
[Table("t_j_produitsimilaire_pds")]
public partial class Produitsimilaire
{
    [Key]
    [Column("pds_idproduit")]
    public int IdproduitRef { get; set; }

    [Key]
    [Column("pds_idproduit2")]
    public int IdproduitSim { get; set; }

    [ForeignKey(nameof(IdproduitRef))]
    [InverseProperty(nameof(Produit.SimilaireRefNavigation))]
    public virtual Produit ProduitRefNavigation { get; set; } = null!;

    [ForeignKey(nameof(IdproduitSim))]
    [InverseProperty(nameof(Produit.SimilaireSimNavigation))]
    public virtual Produit ProduitSimNavigation { get; set; } = null!;
}
