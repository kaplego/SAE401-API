using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_regroupementproduit_rgp")]
public partial class Regroupementproduit
{
    [Key]
    [Column("rgp_idregroupement")]
    public int Idregroupement { get; set; }

    [Column("rgp_nomregroupement")]
    [StringLength(64)]
    public string Nomregroupement { get; set; } = null!;

    [ForeignKey(nameof(Idregroupement))]
    [InverseProperty("Idregroupements")]
    public virtual ICollection<Coloration> Colorations { get; set; } = new List<Coloration>();
}
