using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("pays")]
[Index("Idpays", Name = "pays_pk", IsUnique = true)]
public partial class Pay
{
    [Key]
    [Column("idpays")]
    public int Idpays { get; set; }

    [Column("nompays")]
    [StringLength(32)]
    public string Nompays { get; set; } = null!;

    [InverseProperty("IdpaysNavigation")]
    public virtual ICollection<Adresse> Adresses { get; set; } = new List<Adresse>();

    [InverseProperty("IdpaysNavigation")]
    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
}
