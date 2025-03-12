using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("commande")]
[Index("AdrIdadresse", Name = "adressefacturation_fk")]
[Index("Idadresse", Name = "adresselivraison_fk")]
[Index("Idclient", Name = "clientcommande_fk")]
[Index("Idcodepromo", Name = "codepromocommande_fk")]
[Index("Idcommande", Name = "commande_pk", IsUnique = true)]
[Index("Idstatut", Name = "commandestatut_fk")]
[Index("Idtransporteur", Name = "transportcommande_fk")]
public partial class Commande
{
    [Key]
    [Column("idcommande")]
    public int Idcommande { get; set; }

    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("idadresse")]
    public int Idadresse { get; set; }

    [Column("idcodepromo")]
    public int? Idcodepromo { get; set; }

    [Column("adr_idadresse")]
    public int AdrIdadresse { get; set; }

    [Column("idstatut")]
    public int Idstatut { get; set; }

    [Column("idtransporteur")]
    public int Idtransporteur { get; set; }

    [Column("datecommande")]
    public DateOnly Datecommande { get; set; }

    [Column("avecassurance")]
    public bool Avecassurance { get; set; }

    [Column("aveclivraisonexpress")]
    public bool Aveclivraisonexpress { get; set; }

    [Column("instructionlivraison")]
    [StringLength(512)]
    public string? Instructionlivraison { get; set; }

    [ForeignKey("AdrIdadresse")]
    [InverseProperty("CommandeAdrIdadresseNavigations")]
    public virtual Adresse AdrIdadresseNavigation { get; set; } = null!;

    [InverseProperty("IdcommandeNavigation")]
    public virtual ICollection<Commandecomposition> Commandecompositions { get; set; } = new List<Commandecomposition>();

    [InverseProperty("IdcommandeNavigation")]
    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();

    [ForeignKey("Idadresse")]
    [InverseProperty("CommandeIdadresseNavigations")]
    public virtual Adresse IdadresseNavigation { get; set; } = null!;

    [ForeignKey("Idclient")]
    [InverseProperty("Commandes")]
    public virtual Client IdclientNavigation { get; set; } = null!;

    [ForeignKey("Idcodepromo")]
    [InverseProperty("Commandes")]
    public virtual Codepromo? IdcodepromoNavigation { get; set; }

    [ForeignKey("Idstatut")]
    [InverseProperty("Commandes")]
    public virtual Statutcommande IdstatutNavigation { get; set; } = null!;

    [ForeignKey("Idtransporteur")]
    [InverseProperty("Commandes")]
    public virtual Transporteur IdtransporteurNavigation { get; set; } = null!;

    [InverseProperty("IdcommandeNavigation")]
    public virtual ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();
}
