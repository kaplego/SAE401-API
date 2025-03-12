using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("regroupementproduit")]
[Index("Idregroupement", Name = "regroupementproduit_pk", IsUnique = true)]
public partial class Regroupementproduit
{
    [Key]
    [Column("idregroupement")]
    public int Idregroupement { get; set; }

    [Column("nomregroupement")]
    [StringLength(64)]
    public string Nomregroupement { get; set; } = null!;

    [ForeignKey("Idregroupement")]
    [InverseProperty("Idregroupements")]
    public virtual ICollection<Coloration> Colorations { get; set; } = new List<Coloration>();
}
