using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_categorieproduit_cat")]
[Index(nameof(Nomcategorie), Name = "ix_t_e_categorieproduit_cat_nomcategorie", IsUnique = true)]

public partial class Categorieproduit
{
    [Key]
    [Column("cat_idcategorie")]
    public int Idcategorie { get; set; }



    [Column("cat_idcategorie2")]
    public int? IdcategorieParent { get; set; }

    [Column("cat_idphoto")]
    public int? Idphoto { get; set; }

    [Column("cat_nomcategorie")]
    [StringLength(64)]
    public string Nomcategorie { get; set; } = null!;

    [Column("cat_descriptioncategorie")]
    [StringLength(512)]
    public string? Descriptioncategorie { get; set; }

    [Column("cat_estfiltrable")]
    public bool Estfiltrable { get; set; }



    [ForeignKey(nameof(IdcategorieParent))]
    [InverseProperty(nameof(Categorieproduit.CategorieEnfanteNavigation))]
    public virtual Categorieproduit? CategorieParenteNavigation { get; set; }



    [ForeignKey(nameof(Idphoto))]
    [InverseProperty(nameof(Photo.CategoriesNavigation))]
    public virtual Photo? PhotoNavigation { get; set; }

    [InverseProperty(nameof(Categorieproduit.CategorieParenteNavigation))]
    public virtual ICollection<Categorieproduit> CategorieEnfanteNavigation { get; set; } = new List<Categorieproduit>();


    [InverseProperty(nameof(Typeproduit.CategorieNavigation))]
    public virtual ICollection<Typeproduit> TypesNavigation { get; set; } = new List<Typeproduit>();
}
