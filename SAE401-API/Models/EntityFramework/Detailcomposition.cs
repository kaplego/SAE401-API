using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idproduit), nameof(Idcouleur), nameof(Idcomposition))]
[Table("t_j_detailcomposition_dcp")]
public partial class Detailcomposition
{
    [Key]
    [Column("dcp_idproduit")]
    public int Idproduit { get; set; }

    [Key]
    [Column("dcp_idcouleur")]
    public int Idcouleur { get; set; }

    [Key]
    [Column("dcp_idcomposition")]
    public int Idcomposition { get; set; }

    [Column("dcp_quantitecomposition")]
    public int Quantitecomposition { get; set; }

    [ForeignKey("Idproduit, Idcouleur")]
    [InverseProperty("Detailcompositions")]
    public virtual Coloration Coloration { get; set; } = null!;

    [ForeignKey(nameof(Idcomposition))]
    [InverseProperty("Detailcompositions")]
    public virtual Compositionproduit IdcompositionNavigation { get; set; } = null!;
}
