using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_adresse_adr")]
public partial class Adresse
{
    [Key]
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
    [InverseProperty("Adresses")]
    public virtual Ville CodeinseeNavigation { get; set; } = null!;

    [InverseProperty("AdrIdadresseNavigation")]
    public virtual ICollection<Commande> CommandeAdrIdadresseNavigations { get; set; } = new List<Commande>();

    [InverseProperty("IdadresseNavigation")]
    public virtual ICollection<Commande> CommandeIdadresseNavigations { get; set; } = new List<Commande>();

    [ForeignKey(nameof(Idclient))]
    [InverseProperty("Adresses")]
    public virtual Client IdclientNavigation { get; set; } = null!;

    [ForeignKey(nameof(Iddepartement))]
    [InverseProperty("Adresses")]
    public virtual Departement IddepartementNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idpays))]
    [InverseProperty("Adresses")]
    public virtual Pay IdpaysNavigation { get; set; } = null!;
}
