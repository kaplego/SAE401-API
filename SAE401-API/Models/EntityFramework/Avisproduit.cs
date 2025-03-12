using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Validation;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_avisproduit_avi")]
[Index(nameof(Idclient),nameof(Idproduit), Name = "ix_t_e_avisproduit_avi_idclient_idproduit", IsUnique = true)]

public partial class Avisproduit
{
    [Key]
    [Column("avi_idavis")]
    public int Idavis { get; set; }

    [Column("avi_idproduit")]
    public int Idproduit { get; set; }

    [Column("avi_idclient")]
    public int Idclient { get; set; }

    [Column("avi_noteavis")]
    [Range(minimum: 0, maximum: 4, ErrorMessage = "La note doit être comprise entre 0 et 4")]
    public int Noteavis { get; set; }

    [Column("avi_dateavis")]
    [DateValidator]
    public DateTime Dateavis { get; set; }

    [Column("avi_commentaireavis")]
    [StringLength(1024)]
    public string? Commentaireavis { get; set; }

    [Column("avi_reponsemiliboo")]
    [StringLength(1024)]
    public string? Reponsemiliboo { get; set; }

    [ForeignKey(nameof(Idclient))]
    [InverseProperty("Avisproduits")]
    public virtual Client IdclientNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idproduit))]
    [InverseProperty("Avisproduits")]
    public virtual Produit IdproduitNavigation { get; set; } = null!;

    [InverseProperty("IdavisNavigation")]
    public virtual ICollection<Signalementavi> Signalementavis { get; set; } = new List<Signalementavi>();

    [ForeignKey(nameof(Idavis))]
    [InverseProperty("Idavis")]
    public virtual ICollection<Photo> Idphotos { get; set; } = new List<Photo>();
}
