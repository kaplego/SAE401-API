using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_compositionproduit_cmp")]
[Index(nameof(Nomcomposition), Name = "ix_t_e_compositionproduit_cmp_nomcomposition", IsUnique = true)]

public partial class Compositionproduit
{
    [Key]
    [Column("cmp_idcomposition")]
    public int Idcomposition { get; set; }

    [Column("cmp_nomcomposition")]
    [StringLength(150)]
    public string? Nomcomposition { get; set; }

    [Column("cmp_prixventecomposition", TypeName = "numeric(10, 2)")]
    public decimal Prixventecomposition { get; set; }

    [Column("cmp_prixsoldecomposition", TypeName = "numeric(10, 2)")]
    public decimal? Prixsoldecomposition { get; set; }

    [Column("cmp_descriptioncomposition")]
    [StringLength(2048)]
    public string? Descriptioncomposition { get; set; }

    [InverseProperty("IdcompositionNavigation")]
    public virtual ICollection<Commandecomposition> Commandecompositions { get; set; } = new List<Commandecomposition>();

    [InverseProperty("IdcompositionNavigation")]
    public virtual ICollection<Detailcomposition> Detailcompositions { get; set; } = new List<Detailcomposition>();

    [InverseProperty("Composition")]
    public virtual ICollection<Detailpaniercomposition> Detailpaniercompositions { get; set; } = new List<Detailpaniercomposition>();
}
