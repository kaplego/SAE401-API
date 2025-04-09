using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idproduit), nameof(Idcouleur), nameof(Idclient))]
[Table("t_j_detailpanier_dpn")]
public partial class Detailpanier
{
    [Key]
    [Column("dpn_idproduit")]
    public int Idproduit { get; set; }

    [Key]
    [Column("dpn_idcouleur")]
    public int Idcouleur { get; set; }

    [Key]
    [Column("dpn_idclient")]
    public int Idclient { get; set; }

    [Column("dpn_quantitepanier")]
    [Range(1, int.MaxValue, ErrorMessage = "La valeur doit être supérieure ou égale à 1.")]
    public int Quantitepanier { get; set; }

    [ForeignKey("Idproduit, Idcouleur")]
    [InverseProperty(nameof(Coloration.DetailsPanierNavigation))]
    public virtual Coloration ColorationNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idclient))]
    [InverseProperty(nameof(Client.PaniersProduitNavigation))]
    public virtual Client ClientNavigation { get; set; } = null!;
}
