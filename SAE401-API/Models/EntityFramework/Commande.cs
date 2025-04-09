using SAE401_API.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_commande_cmd")]
public partial class Commande
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("cmd_idcommande")]
    public int Idcommande { get; set; }

    [Column("cmd_idclient")]
    public int Idclient { get; set; }

    [Column("cmd_idadresse")]
    public int IdadresseLivr { get; set; }

    [Column("cmd_idcodepromo")]
    public int? Idcodepromo { get; set; }

    [Column("cmd_adr_idadresse")]
    public int IdadresseFact { get; set; }

    [Column("cmd_idstatut")]
    public int Idstatut { get; set; }

    [Column("cmd_idtransporteur")]
    public int Idtransporteur { get; set; }

    [Column("cmd_datecommande")]
    [DateValidator]
    [Required]
    public DateTime Datecommande { get; set; } = DateTime.UtcNow;

    [Column("cmd_avecassurance")]
    public bool Avecassurance { get; set; }

    [Column("cmd_aveclivraisonexpress")]
    public bool Aveclivraisonexpress { get; set; }

    [Column("cmd_instructionlivraison")]
    [StringLength(512)]
    public string? Instructionlivraison { get; set; }

    [ForeignKey(nameof(IdadresseFact))]
    [InverseProperty(nameof(Adresse.CommandeFactNavigation))]
    public virtual Adresse AdresseFactNavigation { get; set; } = null!;

    [InverseProperty(nameof(Commandecomposition.CommandeNavigation))]
    public virtual ICollection<Commandecomposition> DetailsCompositionNavigation { get; set; } = new List<Commandecomposition>();

    [InverseProperty(nameof(Detailcommande.CommandeNavigation))]
    public virtual ICollection<Detailcommande> DetailsProduitNavigation { get; set; } = new List<Detailcommande>();

    [ForeignKey(nameof(IdadresseLivr))]
    [InverseProperty(nameof(Adresse.CommandeLivrNavigation))]
    public virtual Adresse AdresseLivrNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idclient))]
    [InverseProperty(nameof(Client.CommandesNavigation))]
    public virtual Client ClientNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idcodepromo))]
    [InverseProperty(nameof(Codepromo.CommandesNavigation))]
    public virtual Codepromo? CodeNavigation { get; set; }

    [ForeignKey(nameof(Idstatut))]
    [InverseProperty(nameof(Statutcommande.CommandesNavigation))]
    public virtual Statutcommande StatutNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idtransporteur))]
    [InverseProperty(nameof(Transporteur.CommandesNavigation))]
    public virtual Transporteur TransporteurNavigation { get; set; } = null!;

    [InverseProperty(nameof(Paiement.CommandeNavigation))]
    public virtual ICollection<Paiement> PaiementsNavigation { get; set; } = new List<Paiement>();
}
