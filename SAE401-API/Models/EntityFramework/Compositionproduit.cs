using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("compositionproduit")]
[Index("Idcomposition", Name = "compositionproduit_pk", IsUnique = true)]
public partial class Compositionproduit
{
    [Key]
    [Column("idcomposition")]
    public int Idcomposition { get; set; }

    [Column("prixventecomposition")]
    [Precision(10, 2)]
    public decimal Prixventecomposition { get; set; }

    [Column("prixsoldecomposition")]
    [Precision(10, 2)]
    public decimal? Prixsoldecomposition { get; set; }

    [Column("descriptioncomposition")]
    [StringLength(2048)]
    public string? Descriptioncomposition { get; set; }

    [InverseProperty("IdcompositionNavigation")]
    public virtual ICollection<Commandecomposition> Commandecompositions { get; set; } = new List<Commandecomposition>();

    [InverseProperty("IdcompositionNavigation")]
    public virtual ICollection<Detailcomposition> Detailcompositions { get; set; } = new List<Detailcomposition>();
}
