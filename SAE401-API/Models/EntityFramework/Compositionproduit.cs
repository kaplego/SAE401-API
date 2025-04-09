using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    [Range(0.0, double.MaxValue, ErrorMessage = "La valeur doit être supérieure ou égale à 0.")]
    public decimal Prixventecomposition { get; set; }

    [Column("cmp_prixsoldecomposition", TypeName = "numeric(10, 2)")]
    [Range(0.0, double.MaxValue, ErrorMessage = "La valeur doit être supérieure ou égale à 0.")]
    public decimal? Prixsoldecomposition { get; set; }

    [Column("cmp_descriptioncomposition")]
    [StringLength(2048)]
    public string? Descriptioncomposition { get; set; }

    [InverseProperty(nameof(Commandecomposition.CompositionNavigation))]
    public virtual ICollection<Commandecomposition> CommandesNavigation { get; set; } = new List<Commandecomposition>();

    [InverseProperty(nameof(Detailcomposition.CompositionNavigation))]
    public virtual ICollection<Detailcomposition> DetailsNavigation { get; set; } = new List<Detailcomposition>();

    [InverseProperty(nameof(Detailpaniercomposition.CompositionNavigation))]
    public virtual ICollection<Detailpaniercomposition> PaniersNavigation { get; set; } = new List<Detailpaniercomposition>();
}
