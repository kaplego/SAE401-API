using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("categorieproduit")]
[Index("CatIdcategorie", Name = "categoriecategorie_fk")]
[Index("Idcategorie", Name = "categorieproduit_pk", IsUnique = true)]
[Index("Idphoto", Name = "photocategorie_fk")]
public partial class Categorieproduit
{
    [Key]
    [Column("idcategorie")]
    public int Idcategorie { get; set; }

    [Column("cat_idcategorie")]
    public int? CatIdcategorie { get; set; }

    [Column("idphoto")]
    public int? Idphoto { get; set; }

    [Column("nomcategorie")]
    [StringLength(64)]
    public string Nomcategorie { get; set; } = null!;

    [Column("descriptioncategorie")]
    [StringLength(512)]
    public string? Descriptioncategorie { get; set; }

    [Column("estfiltrable")]
    public bool Estfiltrable { get; set; }

    [ForeignKey("CatIdcategorie")]
    [InverseProperty("InverseCatIdcategorieNavigation")]
    public virtual Categorieproduit? CatIdcategorieNavigation { get; set; }

    [ForeignKey("Idphoto")]
    [InverseProperty("Categorieproduits")]
    public virtual Photo? IdphotoNavigation { get; set; }

    [InverseProperty("CatIdcategorieNavigation")]
    public virtual ICollection<Categorieproduit> InverseCatIdcategorieNavigation { get; set; } = new List<Categorieproduit>();

    [InverseProperty("IdcategorieNavigation")]
    public virtual ICollection<Typeproduit> Typeproduits { get; set; } = new List<Typeproduit>();
}
