using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("client")]
[Index("Idclient", Name = "client_pk", IsUnique = true)]
public partial class Client
{
    [Key]
    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("nomclient")]
    [StringLength(64)]
    public string Nomclient { get; set; } = null!;

    [Column("prenomclient")]
    [StringLength(64)]
    public string Prenomclient { get; set; } = null!;

    [Column("civiliteclient")]
    [MaxLength(1)]
    public char? Civiliteclient { get; set; }

    [Column("emailclient")]
    [StringLength(256)]
    public string Emailclient { get; set; } = null!;

    [Column("telfixeclient")]
    [StringLength(11)]
    public string? Telfixeclient { get; set; }

    [Column("telportableclient")]
    [StringLength(11)]
    public string Telportableclient { get; set; } = null!;

    [Column("datecreationcompte")]
    [StringLength(11)]
    public string Datecreationcompte { get; set; } = null!;

    [Column("hashmdp")]
    [StringLength(256)]
    public string Hashmdp { get; set; } = null!;

    [Column("pointfideliteclient")]
    public int Pointfideliteclient { get; set; }

    [Column("newslettermiliboo")]
    public bool Newslettermiliboo { get; set; }

    [Column("newsletterpartenaires")]
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

    [ForeignKey("Idclient")]
    [InverseProperty("Idclients")]
    public virtual ICollection<Produit> Idproduits { get; set; } = new List<Produit>();
}
