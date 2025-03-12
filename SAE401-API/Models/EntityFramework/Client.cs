using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_client_cli")]
public partial class Client
{
    [Key]
    [Column("cli_idclient")]
    public int Idclient { get; set; }

    [Column("cli_nomclient")]
    [StringLength(64)]
    public string Nomclient { get; set; } = null!;

    [Column("cli_prenomclient")]
    [StringLength(64)]
    public string Prenomclient { get; set; } = null!;

    [Column("cli_civiliteclient")]
    [MaxLength(1)]
    public char? Civiliteclient { get; set; }

    [Column("cli_emailclient")]
    [StringLength(256)]
    public string Emailclient { get; set; } = null!;

    [Column("cli_telfixeclient")]
    [StringLength(11)]
    public string? Telfixeclient { get; set; }

    [Column("cli_telportableclient")]
    [StringLength(11)]
    public string Telportableclient { get; set; } = null!;

    [Column("cli_datecreationcompte")]
    public DateTime? Datecreationcompte { get; set; } = null!;

    [Column("cli_hashmdp")]
    [StringLength(256)]
    public string Hashmdp { get; set; } = null!;

    [Column("cli_pointfideliteclient")]
    public int Pointfideliteclient { get; set; }

    [Column("cli_newslettermiliboo")]
    public bool Newslettermiliboo { get; set; }

    [Column("cli_newsletterpartenaires")]
    public bool Newsletterpartenaires { get; set; }

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Adresse> Adresses { get; set; } = new List<Adresse>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Avisproduit> Avisproduits { get; set; } = new List<Avisproduit>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Cartebancaire> Cartebancaires { get; set; } = new List<Cartebancaire>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Codepromo> Codepromos { get; set; } = new List<Codepromo>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Detailpanier> Detailpaniers { get; set; } = new List<Detailpanier>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Historiqueconsultation> Historiqueconsultations { get; set; } = new List<Historiqueconsultation>();

    [InverseProperty("IdclientNavigation")]
    public virtual ICollection<Messagechatbot> Messagechatbots { get; set; } = new List<Messagechatbot>();

    [InverseProperty("IdclientNavigation")]
    public virtual Professionel? Professionel { get; set; }

    [ForeignKey(nameof(Idclient))]
    [InverseProperty("Idclients")]
    public virtual ICollection<Produit> Idproduits { get; set; } = new List<Produit>();
}
