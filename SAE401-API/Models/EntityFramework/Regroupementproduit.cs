using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
