using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idproduit), nameof(Idcouleur), nameof(Idregroupement))]
[Table("t_j_detailregroupement_drg")]
public partial class Detailregroupement
{
    [Key]
    [Column("drg_idproduit")]
    public int Idproduit { get; set; }

    [Key]
    [Column("drg_idcouleur")]
    public int Idcouleur { get; set; }

    [Key]
    [Column("drg_idregroupement")]
    public int Idregroupement { get; set; }

    [ForeignKey("Idproduit, Idcouleur")]
    [InverseProperty(nameof(Coloration.DetailRegroupementNavigation))]
    public virtual Coloration ColorationsNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idregroupement))]
    [InverseProperty(nameof(Regroupementproduit.DetailsNavigation))]
    public virtual Regroupementproduit RegroupementNavigation { get; set; } = null!;
}
