using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("avisproduit")]
[Index("Idclient", Name = "avisclient_fk")]
[Index("Idproduit", Name = "avispourproduit_fk")]
[Index("Idavis", Name = "avisproduit_pk", IsUnique = true)]
public partial class Avisproduit
{
    [Key]
    [Column("idavis")]
    public int Idavis { get; set; }

    [Column("idproduit")]
    public int Idproduit { get; set; }

    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("noteavis")]
    public int Noteavis { get; set; }

    [Column("dateavis")]
    public DateOnly Dateavis { get; set; }

    [Column("commentaireavis")]
    [StringLength(1024)]
    public string? Commentaireavis { get; set; }

    [Column("reponsemiliboo")]
    [StringLength(1024)]
    public string? Reponsemiliboo { get; set; }

    [ForeignKey("Idclient")]
    [InverseProperty("Avisproduits")]
    public virtual Client IdclientNavigation { get; set; } = null!;

    [ForeignKey("Idproduit")]
    [InverseProperty("Avisproduits")]
    public virtual Produit IdproduitNavigation { get; set; } = null!;

    [InverseProperty("IdavisNavigation")]
    public virtual ICollection<Signalementavi> Signalementavis { get; set; } = new List<Signalementavi>();

    [ForeignKey("Idavis")]
    [InverseProperty("Idavis")]
    public virtual ICollection<Photo> Idphotos { get; set; } = new List<Photo>();
}
