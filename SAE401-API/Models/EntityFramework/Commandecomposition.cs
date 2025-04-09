using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idcomposition), nameof(Idcommande))]
[Table("t_j_commandecomposition_cmc")]
public partial class Commandecomposition
{
    [Key]
    [Column("cmc_idcomposition")]
    public int Idcomposition { get; set; }

    [Key]
    [Column("cmc_idcommande")]
    public int Idcommande { get; set; }

    [Column("cmc_quantitecompositioncommande")]
    public int Quantitecompositioncommande { get; set; }

    [ForeignKey(nameof(Idcommande))]
    [InverseProperty(nameof(Commande.DetailsCompositionNavigation))]
    public virtual Commande CommandeNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idcomposition))]
    [InverseProperty(nameof(Compositionproduit.CommandesNavigation))]
    public virtual Compositionproduit CompositionNavigation { get; set; } = null!;
}
