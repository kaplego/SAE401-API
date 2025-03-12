using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idcomposition), nameof(Idclient))]
[Table("t_j_detailpanier_dpc")]
public partial class Detailpaniercomposition
{
    [Key]
    [Column("dpc_idcomposition")]
    public int Idcomposition { get; set; }

    [Key]
    [Column("dpc_idclient")]
    public int Idclient { get; set; }

    [Column("dpc_quantitepaniercomposition")]
    public int Quantitepaniercomposition { get; set; }

    [ForeignKey(nameof(Idcomposition))]
    [InverseProperty("Detailpaniercompositions")]
    public virtual Compositionproduit Composition { get; set; } = null!;

    [ForeignKey(nameof(Idclient))]
    [InverseProperty("Detailpaniercompositions")]
    public virtual Client IdclientNavigation { get; set; } = null!;
}
