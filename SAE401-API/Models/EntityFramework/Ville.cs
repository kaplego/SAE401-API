using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_ville_vil")]
public partial class Ville
{
    [Key]
    [Column("vil_codeinsee")]
    [StringLength(5)]
    public string Codeinsee { get; set; } = null!;

    [Column("vil_nomville")]
    [StringLength(50)]
    public string? Nomville { get; set; }

    [InverseProperty(nameof(Adresse.VilleNavigation))]
    public virtual ICollection<Adresse> AdressesNavigation { get; set; } = new List<Adresse>();
}
