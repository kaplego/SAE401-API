using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_regroupementproduit_rgp")]
[Index(nameof(Nomregroupement), Name = "ix_t_e_regroupementproduit_rgp_nomregroupement", IsUnique = true)]

public partial class Regroupementproduit
{
    [Key]
    [Column("rgp_idregroupement")]
    public int Idregroupement { get; set; }

    [Column("rgp_nomregroupement")]
    [StringLength(64)]
    public string Nomregroupement { get; set; } = null!;

    [InverseProperty(nameof(Detailregroupement.RegroupementNavigation))]
    public virtual ICollection<Detailregroupement> DetailsNavigation { get; set; } = new List<Detailregroupement>();
}
