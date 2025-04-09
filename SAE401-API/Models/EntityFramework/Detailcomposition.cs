using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idproduit), nameof(Idcouleur), nameof(Idcomposition))]
[Table("t_j_detailcomposition_dcp")]
public partial class Detailcomposition
{
    [Key]
    [Column("dcp_idproduit")]
    public int Idproduit { get; set; }

    [Key]
    [Column("dcp_idcouleur")]
    public int Idcouleur { get; set; }

    [Key]
    [Column("dcp_idcomposition")]
    public int Idcomposition { get; set; }

    [Column("dcp_quantitecomposition")]
    [Range(1, int.MaxValue, ErrorMessage = "La valeur doit être supérieure ou égale à 1.")]
    public int Quantitecomposition { get; set; }

    [ForeignKey("Idproduit, Idcouleur")]
    [InverseProperty(nameof(ColorationNavigation.DetailsCompositionNavigation))]
    public virtual Coloration ColorationNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idcomposition))]
    [InverseProperty(nameof(Compositionproduit.DetailsNavigation))]
    public virtual Compositionproduit CompositionNavigation { get; set; } = null!;
}
