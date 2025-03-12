using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("ville")]
[Index("Codeinsee", Name = "ville_pk", IsUnique = true)]
public partial class Ville
{
    [Key]
    [Column("codeinsee")]
    [StringLength(5)]
    public string Codeinsee { get; set; } = null!;

    [Column("nomville")]
    [StringLength(50)]
    public string? Nomville { get; set; }

    [InverseProperty("CodeinseeNavigation")]
    public virtual ICollection<Adresse> Adresses { get; set; } = new List<Adresse>();
}
