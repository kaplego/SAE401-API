using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_pays_pay")]
[Index(nameof(Nompays), Name = "ix_t_e_pays_pay_nompays", IsUnique = true)]

public partial class Pay
{
    [Key]
    [Column("pay_idpays")]
    public int Idpays { get; set; }

    [Column("pay_nompays")]
    [StringLength(32)]
    public string Nompays { get; set; } = null!;

    [InverseProperty(nameof(Adresse.PayNavigation))]
    public virtual ICollection<Adresse> AdressesNavigation { get; set; } = new List<Adresse>();

    [InverseProperty(nameof(Produit.PayNavigation))]
    public virtual ICollection<Produit> ProduitsNavigation { get; set; } = new List<Produit>();
}
