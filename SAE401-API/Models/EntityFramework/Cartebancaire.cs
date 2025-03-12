using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("cartebancaire")]
[Index("Idcartebancaire", Name = "cartebancaire_pk", IsUnique = true)]
[Index("Idclient", Name = "carteclient_fk")]
public partial class Cartebancaire
{
    [Key]
    [Column("idcartebancaire")]
    public int Idcartebancaire { get; set; }

    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("nomcartebancaire")]
    [StringLength(32)]
    public string? Nomcartebancaire { get; set; }

    [Column("dateenregistement")]
    public DateOnly Dateenregistement { get; set; }

    [Column("numcartebancaire")]
    [StringLength(16)]
    public string Numcartebancaire { get; set; } = null!;

    [Column("dateexpirationcarte")]
    public DateOnly Dateexpirationcarte { get; set; }

    [ForeignKey("Idclient")]
    [InverseProperty("Cartebancaires")]
    public virtual Client IdclientNavigation { get; set; } = null!;

    [InverseProperty("IdcartebancaireNavigation")]
    public virtual ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();
}
