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
    [Range(1, int.MaxValue, ErrorMessage = "La valeur doit être supérieure ou égale à 1.")]
    public int Quantitecommande { get; set; }

    [ForeignKey("Idproduit, Idcouleur")]
    [InverseProperty(nameof(ColorationNavigation.DetailsCommandeNavigation))]
    public virtual Coloration ColorationNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idcommande))]
    [InverseProperty(nameof(Commande.DetailsProduitNavigation))]
    public virtual Commande CommandeNavigation { get; set; } = null!;
}
