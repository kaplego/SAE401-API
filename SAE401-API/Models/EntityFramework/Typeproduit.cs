using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("typeproduit")]
[Index("Idcategorie", Name = "categorietypeproduit_fk")]
[Index("Idtypeproduit", Name = "typeproduit_pk", IsUnique = true)]
public partial class Typeproduit
{
    [Key]
    [Column("idtypeproduit")]
    public int Idtypeproduit { get; set; }

    [Column("idcategorie")]
    public int Idcategorie { get; set; }

    [Column("nomtypeproduit")]
    [StringLength(64)]
    public string Nomtypeproduit { get; set; } = null!;

    [InverseProperty("IdtypeproduitNavigation")]
    public virtual ICollection<Attributproduit> Attributproduits { get; set; } = new List<Attributproduit>();

    [ForeignKey("Idcategorie")]
    [InverseProperty("Typeproduits")]
    public virtual Categorieproduit IdcategorieNavigation { get; set; } = null!;

    [InverseProperty("IdtypeproduitNavigation")]
    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
}
