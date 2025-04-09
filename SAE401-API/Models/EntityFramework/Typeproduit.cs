using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_typeproduit_tpd")]
[Index(nameof(Nomtypeproduit), Name = "ix_t_e_typeproduit_tpd_nomtypeproduit", IsUnique = true)]

public partial class Typeproduit
{
    [Key]
    [Column("tpd_idtypeproduit")]
    public int Idtypeproduit { get; set; }

    [Column("tpd_idcategorie")]
    public int Idcategorie { get; set; }

    [Column("tpd_nomtypeproduit")]
    [StringLength(64)]
    public string Nomtypeproduit { get; set; } = null!;

    [InverseProperty(nameof(Attributproduit.TypeproduitNavigation))]
    public virtual ICollection<Attributproduit> AttributsNavigation { get; set; } = new List<Attributproduit>();

    [ForeignKey(nameof(Idcategorie))]
    [InverseProperty(nameof(Categorieproduit.TypesNavigation))]
    public virtual Categorieproduit CategorieNavigation { get; set; } = null!;

    [InverseProperty(nameof(Produit.TypeNavigation))]
    public virtual ICollection<Produit> ProduitsNavigation { get; set; } = new List<Produit>();
}
