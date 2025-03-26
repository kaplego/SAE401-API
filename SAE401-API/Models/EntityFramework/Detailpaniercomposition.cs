using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idcomposition), nameof(Idclient))]
[Table("t_j_detailpaniercomposition_dpc")]
public partial class Detailpaniercomposition
{
    [Key]
    [Column("dpc_idcomposition")]
    public int Idcomposition { get; set; }

    [Key]
    [Column("dpc_idclient")]
    public int Idclient { get; set; }

    [Column("dpc_quantitepaniercomposition")]
    [Range(1, int.MaxValue, ErrorMessage = "La valeur doit être supérieure ou égale à 1.")]
    public int Quantitepaniercomposition { get; set; }

    [ForeignKey(nameof(Idcomposition))]
    [InverseProperty(nameof(Compositionproduit.PaniersNavigation))]
    public virtual Compositionproduit CompositionNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idclient))]
    [InverseProperty(nameof(Client.PaniersCompositionNavigation))]
    public virtual Client ClientNavigation { get; set; } = null!;
}
