using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_adresse_adr")]
public partial class Adresse
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("adr_idadresse")]
    public int Idadresse { get; set; }

    [Column("adr_idpays")]
    public int Idpays { get; set; }

    [Column("adr_codeinsee")]
    [StringLength(5)]
    public string Codeinsee { get; set; } = null!;

    [Column("adr_idclient")]
    public int Idclient { get; set; }

    [Column("adr_iddepartement")]
    public int Iddepartement { get; set; }

    [Column("adr_nomadresse")]
    [StringLength(32)]
    public string? Nomadresse { get; set; }

    [Column("adr_numerorue")]
    [StringLength(8)]
    public string? Numerorue { get; set; }

    [Column("adr_nomrue")]
    [StringLength(128)]
    public string Nomrue { get; set; } = null!;

    [Column("adr_codepostaladresse")]
    [StringLength(5)]
    [RegularExpression(@"^[0-9][A0-9][0-9]{3}$", ErrorMessage = "Le code postale doit commencer par un chiffre, suivi d'un 'A' ou d'un chiffre, puis de 3 autres chiffres.")]

    public string Codepostaladresse { get; set; } = null!;

    [ForeignKey(nameof(Codeinsee))]
    [InverseProperty(nameof(Ville.AdressesNavigation))]
    public virtual Ville VilleNavigation { get; set; } = null!;

    [InverseProperty(nameof(Commande.AdresseLivrNavigation))]
    [JsonIgnore]
    public virtual ICollection<Commande> CommandeLivrNavigation { get; set; } = new List<Commande>();

    [InverseProperty(nameof(Commande.AdresseFactNavigation))]
    [JsonIgnore]
    public virtual ICollection<Commande> CommandeFactNavigation { get; set; } = new List<Commande>();

    [ForeignKey(nameof(Idclient))]
    [InverseProperty(nameof(Client.AdressesNavigation))]
    public virtual Client ClientNavigation { get; set; } = null!;

    [ForeignKey(nameof(Iddepartement))]
    [InverseProperty(nameof(Departement.AdressesNavigation))]
    public virtual Departement DepartementNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idpays))]
    [InverseProperty(nameof(Pay.AdressesNavigation))]
    public virtual Pay PayNavigation { get; set; } = null!;
}
