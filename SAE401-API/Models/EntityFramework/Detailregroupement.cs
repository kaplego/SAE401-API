using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idproduit), nameof(Idcouleur), nameof(Idregroupement))]
[Table("t_j_detailregroupement_drg")]
public partial class Detailregroupement
{
    [Key]
    [Column("drg_idproduit")]
    public int Idproduit { get; set; }

    [Key]
    [Column("drg_idcouleur")]
    public int Idcouleur { get; set; }

    [Key]
    [Column("drg_idregroupement")]
    public int Idregroupement { get; set; }

    [ForeignKey("Idproduit, Idcouleur")]
    [InverseProperty("Detailregroupements")]
    public virtual Coloration Colorations { get; set; } = null!;

    [ForeignKey(nameof(Idregroupement))]
    [InverseProperty("Detailregroupements")]
    public virtual Regroupementproduit IdregroupementNavigation { get; set; } = null!;
}
