using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idproduit), nameof(Idcouleur), nameof(Idphoto))]
[Table("t_j_photocoloration_pco")]
public partial class Photocoloration
{
    [Key]
    [Column("pco_idproduit")]
    public int Idproduit { get; set; }

    [Key]
    [Column("pco_idcouleur")]
    public int Idcouleur { get; set; }

    [Key]
    [Column("pco_idphoto")]
    public int Idphoto { get; set; }

    [ForeignKey("Idproduit, Idcouleur")]
    [InverseProperty(nameof(Coloration.PhotocolsNavigation))]
    public virtual Coloration ColorationNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idphoto))]
    [InverseProperty(nameof(Photo.PhotocolsNavigation))]
    public virtual Photo PhotoNavigation { get; set; } = null!;
}
