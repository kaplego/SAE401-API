using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("transporteur")]
[Index("Idtransporteur", Name = "transporteur_pk", IsUnique = true)]
public partial class Transporteur
{
    [Key]
    [Column("idtransporteur")]
    public int Idtransporteur { get; set; }

    [Column("nomtransporteur")]
    [StringLength(64)]
    public string Nomtransporteur { get; set; } = null!;

    [Column("attribut_105")]
    [StringLength(512)]
    public string? Attribut105 { get; set; }

    [InverseProperty("IdtransporteurNavigation")]
    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
}
