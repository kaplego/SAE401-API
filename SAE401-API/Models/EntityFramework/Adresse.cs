using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("adresse")]
[Index("Idadresse", Name = "adresse_pk", IsUnique = true)]
[Index("Idclient", Name = "adresseclient_fk")]
[Index("Idpays", Name = "adressepays_fk")]
[Index("Iddepartement", Name = "estsitue_fk")]
[Index("Codeinsee", Name = "residedans_fk")]
public partial class Adresse
{
    [Key]
    [Column("idadresse")]
    public int Idadresse { get; set; }

    [Column("idpays")]
    public int Idpays { get; set; }

    [Column("codeinsee")]
    [StringLength(5)]
    public string Codeinsee { get; set; } = null!;

    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("iddepartement")]
    public int Iddepartement { get; set; }

    [Column("nomadresse")]
    [StringLength(32)]
    public string? Nomadresse { get; set; }

    [Column("numerorue")]
    [StringLength(8)]
    public string? Numerorue { get; set; }

    [Column("nomrue")]
    [StringLength(128)]
    public string Nomrue { get; set; } = null!;

    [Column("codepostaladresse")]
    [StringLength(5)]
    public string Codepostaladresse { get; set; } = null!;

    [ForeignKey("Codeinsee")]
    [InverseProperty("Adresses")]
    public virtual Ville CodeinseeNavigation { get; set; } = null!;

    [InverseProperty("AdrIdadresseNavigation")]
    public virtual ICollection<Commande> CommandeAdrIdadresseNavigations { get; set; } = new List<Commande>();

    [InverseProperty("IdadresseNavigation")]
    public virtual ICollection<Commande> CommandeIdadresseNavigations { get; set; } = new List<Commande>();

    [ForeignKey("Idclient")]
    [InverseProperty("Adresses")]
    public virtual Client IdclientNavigation { get; set; } = null!;

    [ForeignKey("Iddepartement")]
    [InverseProperty("Adresses")]
    public virtual Departement IddepartementNavigation { get; set; } = null!;

    [ForeignKey("Idpays")]
    [InverseProperty("Adresses")]
    public virtual Pay IdpaysNavigation { get; set; } = null!;
}
