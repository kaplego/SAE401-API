using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idproduit), nameof(Idcouleur), nameof(Idclient))]
[Table("t_j_detailpanier_dpn")]
public partial class Detailpanier
{
    [Key]
    [Column("dpn_idproduit")]
    public int Idproduit { get; set; }

    [Key]
    [Column("dpn_idcouleur")]
    public int Idcouleur { get; set; }

    [Key]
    [Column("dpn_idclient")]
    public int Idclient { get; set; }

    [Column("dpn_quantitepanier")]
    [Range(1, int.MaxValue, ErrorMessage = "La valeur doit être supérieure ou égale à 1.")]
    public int Quantitepanier { get; set; }

    [ForeignKey("Idproduit, Idcouleur")]
    [InverseProperty("Detailpaniers")]
    public virtual Coloration Coloration { get; set; } = null!;

    [ForeignKey(nameof(Idclient))]
    [InverseProperty("Detailpaniers")]
    public virtual Client IdclientNavigation { get; set; } = null!;
}
