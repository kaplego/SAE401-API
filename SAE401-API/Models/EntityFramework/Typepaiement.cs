using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("typepaiement")]
[Index("Idtypepaiement", Name = "typepaiement_pk", IsUnique = true)]
public partial class Typepaiement
{
    [Key]
    [Column("idtypepaiement")]
    public int Idtypepaiement { get; set; }

    [Column("nomtypepaiement")]
    [StringLength(64)]
    public string Nomtypepaiement { get; set; } = null!;

    [InverseProperty("IdtypepaiementNavigation")]
    public virtual ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();
}
