using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idproduit), nameof(Idcouleur), nameof(Idcommande))]
[Table("t_j_detailcommande_dcm")]
public partial class Detailcommande
{
    [Key]
    [Column("dcm_idproduit")]
    public int Idproduit { get; set; }

    [Key]
    [Column("dcm_idcouleur")]
    public int Idcouleur { get; set; }

    [Key]
    [Column("dcm_idcommande")]
    public int Idcommande { get; set; }

    [Column("dcm_quantitecommande")]
    public int Quantitecommande { get; set; }

    [ForeignKey("Idproduit, Idcouleur")]
    [InverseProperty("Detailcommandes")]
    public virtual Coloration Coloration { get; set; } = null!;

    [ForeignKey(nameof(Idcommande))]
    [InverseProperty("Detailcommandes")]
    public virtual Commande IdcommandeNavigation { get; set; } = null!;
}
