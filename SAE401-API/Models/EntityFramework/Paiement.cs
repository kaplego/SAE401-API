using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("paiement")]
[Index("Idpaiement", Name = "paiement_pk", IsUnique = true)]
[Index("Idcommande", Name = "paiementcommande_fk")]
[Index("Idtypepaiement", Name = "paiementtypepaiement_fk")]
[Index("Idcartebancaire", Name = "utilise_fk")]
public partial class Paiement
{
    [Key]
    [Column("idpaiement")]
    public int Idpaiement { get; set; }

    [Column("idcartebancaire")]
    public int? Idcartebancaire { get; set; }

    [Column("idcommande")]
    public int Idcommande { get; set; }

    [Column("idtypepaiement")]
    public int Idtypepaiement { get; set; }

    [Column("datepaiement")]
    public DateOnly Datepaiement { get; set; }

    [Column("montantpaiement")]
    [Precision(10, 2)]
    public decimal Montantpaiement { get; set; }

    [Column("indicepaiement")]
    [StringLength(256)]
    public string? Indicepaiement { get; set; }

    [ForeignKey("Idcartebancaire")]
    [InverseProperty("Paiements")]
    public virtual Cartebancaire? IdcartebancaireNavigation { get; set; }

    [ForeignKey("Idcommande")]
    [InverseProperty("Paiements")]
    public virtual Commande IdcommandeNavigation { get; set; } = null!;

    [ForeignKey("Idtypepaiement")]
    [InverseProperty("Paiements")]
    public virtual Typepaiement IdtypepaiementNavigation { get; set; } = null!;
}
