using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_typeproduit_tpd")]
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

    [InverseProperty("IdtypeproduitNavigation")]
    public virtual ICollection<Attributproduit> Attributproduits { get; set; } = new List<Attributproduit>();

    [ForeignKey(nameof(Idcategorie))]
    [InverseProperty("Typeproduits")]
    public virtual Categorieproduit IdcategorieNavigation { get; set; } = null!;

    [InverseProperty("IdtypeproduitNavigation")]
    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
}
