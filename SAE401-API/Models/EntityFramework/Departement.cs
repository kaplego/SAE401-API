using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("departement")]
[Index("Iddepartement", Name = "departement_pk", IsUnique = true)]
public partial class Departement
{
    [Key]
    [Column("iddepartement")]
    public int Iddepartement { get; set; }

    [Column("nomdepartement")]
    [StringLength(50)]
    public string? Nomdepartement { get; set; }

    [InverseProperty("IddepartementNavigation")]
    public virtual ICollection<Adresse> Adresses { get; set; } = new List<Adresse>();
}
