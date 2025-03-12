using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_commande_cmd")]
public partial class Commande
{
    [Key]
    [Column("cmd_idcommande")]
    public int Idcommande { get; set; }

    [Column("cmd_idclient")]
    public int Idclient { get; set; }

    [Column("cmd_idadresse")]
    public int Idadresse { get; set; }

    [Column("cmd_idcodepromo")]
    public int? Idcodepromo { get; set; }

    [Column("cmd_adr_idadresse")]
    public int AdrIdadresse { get; set; }

    [Column("cmd_idstatut")]
    public int Idstatut { get; set; }

    [Column("cmd_idtransporteur")]
    public int Idtransporteur { get; set; }

    [Column("cmd_datecommande")]
    public DateOnly Datecommande { get; set; }

    [Column("cmd_avecassurance")]
    public bool Avecassurance { get; set; }

    [Column("cmd_aveclivraisonexpress")]
    public bool Aveclivraisonexpress { get; set; }

    [Column("cmd_instructionlivraison")]
    [StringLength(512)]
    public string? Instructionlivraison { get; set; }

    [ForeignKey(nameof(AdrIdadresse))]
    [InverseProperty("CommandeAdrIdadresseNavigations")]
    public virtual Adresse AdrIdadresseNavigation { get; set; } = null!;

    [InverseProperty("IdcommandeNavigation")]
    public virtual ICollection<Commandecomposition> Commandecompositions { get; set; } = new List<Commandecomposition>();

    [InverseProperty("IdcommandeNavigation")]
    public virtual ICollection<Detailcommande> Detailcommandes { get; set; } = new List<Detailcommande>();

    [ForeignKey(nameof(Idadresse))]
    [InverseProperty("CommandeIdadresseNavigations")]
    public virtual Adresse IdadresseNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idclient))]
    [InverseProperty("Commandes")]
    public virtual Client IdclientNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idcodepromo))]
    [InverseProperty("Commandes")]
    public virtual Codepromo? IdcodepromoNavigation { get; set; }

    [ForeignKey(nameof(Idstatut))]
    [InverseProperty("Commandes")]
    public virtual Statutcommande IdstatutNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idtransporteur))]
    [InverseProperty("Commandes")]
    public virtual Transporteur IdtransporteurNavigation { get; set; } = null!;

    [InverseProperty("IdcommandeNavigation")]
    public virtual ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();
}
