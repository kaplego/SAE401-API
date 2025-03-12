using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_categorieproduit_cat")]
[Index(nameof(Nomcategorie), Name = "ix_t_e_categorieproduit_cat_nomcategorie", IsUnique = true)]

public partial class Categorieproduit
{
    [Key]
    [Column("cat_idcategorie")]
    public int Idcategorie { get; set; }

    [Column("cat_idcategorie2")]
    public int? CatIdcategorie { get; set; }

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

    [ForeignKey(nameof(CatIdcategorie))]
    [InverseProperty("InverseCatIdcategorieNavigation")]
    public virtual Categorieproduit? CatIdcategorieNavigation { get; set; }

    [ForeignKey(nameof(Idphoto))]
    [InverseProperty("Categorieproduits")]
    public virtual Photo? IdphotoNavigation { get; set; }

    [InverseProperty("CatIdcategorieNavigation")]
    public virtual ICollection<Categorieproduit> InverseCatIdcategorieNavigation { get; set; } = new List<Categorieproduit>();

    [InverseProperty("IdcategorieNavigation")]
    public virtual ICollection<Typeproduit> Typeproduits { get; set; } = new List<Typeproduit>();
}
