using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_client_cli")]
[Index(nameof(Nomclient),nameof(Prenomclient),nameof(Telportableclient), Name = "ix_t_e_client_cli_nomclient_prenomclient_telportableclient", IsUnique = true)]

public partial class Client
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("cli_idclient")]
    public int Idclient { get; set; }

    [Column("cli_nomclient")]
    [StringLength(64)]
    public string Nomclient { get; set; } = null!;

    [Column("cli_prenomclient")]
    [StringLength(64)]
    public string Prenomclient { get; set; } = null!;

    [Column("cli_civiliteclient")]
    [RegularExpression(@"^(|[FHX]{1})$", ErrorMessage = "Le caractère doit être F, H ou X.")]
    public char? Civiliteclient { get; set; }

    [Column("cli_emailclient")]
    [StringLength(256)]
    [EmailAddress]
    public string Emailclient { get; set; } = null!;

    [Column("cli_telfixeclient")]
    [StringLength(11)]
    [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Le numéro de fixe doit contenir 11 chiffres.")]
    public string? Telfixeclient { get; set; }

    [Column("cli_telportableclient")]
    [StringLength(11)]
    [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Le numéro de mobile doit contenir 11 chiffres.")]
    public string Telportableclient { get; set; } = null!;

    [Column("cli_datecreationcompte")]
    [Required]
    public DateTime? Datecreationcompte { get; set; } =DateTime.Now;

    [Column("cli_hashmdp")]
    [StringLength(256)]
    [JsonIgnore]
    public string Hashmdp { get; set; } = null!;

    [Column("cli_pointfideliteclient")]
    [Range(0, int.MaxValue, ErrorMessage = "La valeur doit être supérieure ou égale à 0.")]
    public int Pointfideliteclient { get; set; }

    [Column("cli_newslettermiliboo")]
    public bool Newslettermiliboo { get; set; }

    [Column("cli_newsletterpartenaires")]
    public bool Newsletterpartenaires { get; set; }

    [InverseProperty(nameof(Adresse.ClientNavigation))]
    public virtual ICollection<Adresse> AdressesNavigation { get; set; } = new List<Adresse>();

    [InverseProperty(nameof(Avisproduit.ClientNavigation))]
    public virtual ICollection<Avisproduit> AvisNavigation { get; set; } = new List<Avisproduit>();

    [InverseProperty(nameof(Cartebancaire.ClientNavigation))]
    public virtual ICollection<Cartebancaire> CartesNavigation { get; set; } = new List<Cartebancaire>();

    [InverseProperty(nameof(Codepromo.ClientNavigation))]
    public virtual ICollection<Codepromo> CodesNavigation { get; set; } = new List<Codepromo>();

    [InverseProperty(nameof(Commande.ClientNavigation))]
    public virtual ICollection<Commande> CommandesNavigation { get; set; } = new List<Commande>();

    [InverseProperty(nameof(Detailpanier.ClientNavigation))]
    public virtual ICollection<Detailpanier> PaniersProduitNavigation { get; set; } = new List<Detailpanier>();

    [InverseProperty(nameof(Aime.ClientNavigation))]
    public virtual ICollection<Aime> AimesNavigation { get; set; } = new List<Aime>();

    [InverseProperty(nameof(Detailpaniercomposition.ClientNavigation))]
    public virtual ICollection<Detailpaniercomposition> PaniersCompositionNavigation { get; set; } = new List<Detailpaniercomposition>();

    [InverseProperty(nameof(Historiqueconsultation.ClientNavigation))]
    public virtual ICollection<Historiqueconsultation> HistoriquesNavigation { get; set; } = new List<Historiqueconsultation>();

    [InverseProperty(nameof(Messagechatbot.ClientNavigation))]
    public virtual ICollection<Messagechatbot> MessagesNavigation { get; set; } = new List<Messagechatbot>();

    [InverseProperty(nameof(Professionel.ClientNavigation))]
    public virtual Professionel? ProfessionelNavigation { get; set; }
}
