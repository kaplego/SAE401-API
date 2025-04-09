using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idattribut), nameof(Idproduit))]
[Table("t_j_valeurattribut_val")]
public partial class Valeurattribut
{
    [Key]
    [Column("val_idattribut")]
    public int Idattribut { get; set; }

    [Key]
    [Column("val_idproduit")]
    public int Idproduit { get; set; }

    [Column("val_valeur")]
    [StringLength(64)]
    public string Valeur { get; set; } = null!;

    [ForeignKey(nameof(Idattribut))]
    [InverseProperty(nameof(Attributproduit.ValeursNavigation))]
    public virtual Attributproduit AttributNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idproduit))]
    [InverseProperty(nameof(Produit.ValeursNavigation))]
    public virtual Produit ProduitNavigation { get; set; } = null!;
}
