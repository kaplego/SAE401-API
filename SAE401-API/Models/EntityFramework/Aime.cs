using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idclient), nameof(Idproduit))]
[Table("t_j_aime_aim")]
public partial class Aime
{
    [Key]
    [Column("aim_idclient")]
    public int Idclient { get; set; }

    [Key]
    [Column("aim_idproduit")]
    public int Idproduit { get; set; }

    [ForeignKey(nameof(Idclient))]
    [InverseProperty(nameof(Client.AimesNavigation))]
    public virtual Client ClientNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idproduit))]
    [InverseProperty(nameof(Produit.AimesNavigation))]
    public virtual Produit ProduitNavigation { get; set; } = null!;
}
