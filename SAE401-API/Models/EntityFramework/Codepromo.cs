using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("codepromo")]
[Index("Idclient", Name = "clientcodepromo_fk")]
[Index("Idcodepromo", Name = "codepromo_pk", IsUnique = true)]
public partial class Codepromo
{
    [Key]
    [Column("idcodepromo")]
    public int Idcodepromo { get; set; }

    [Column("idclient")]
    public int? Idclient { get; set; }

    [Column("nomcodepromo")]
    [StringLength(16)]
    public string Nomcodepromo { get; set; } = null!;

    [Column("valeurreduction")]
    [Precision(5, 2)]
    public decimal Valeurreduction { get; set; }

    [Column("estvalide")]
    public bool Estvalide { get; set; }

    [Column("dateexpirationcode")]
    public DateOnly? Dateexpirationcode { get; set; }

    [InverseProperty("IdcodepromoNavigation")]
    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    [ForeignKey("Idclient")]
    [InverseProperty("Codepromos")]
    public virtual Client? IdclientNavigation { get; set; }
}
