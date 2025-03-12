using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("couleur")]
[Index("Idcouleur", Name = "couleur_pk", IsUnique = true)]
public partial class Couleur
{
    [Key]
    [Column("idcouleur")]
    public int Idcouleur { get; set; }

    [Column("nomcouleur")]
    [StringLength(64)]
    public string Nomcouleur { get; set; } = null!;

    [Column("rgbcouleur")]
    [StringLength(6)]
    public string Rgbcouleur { get; set; } = null!;

    [InverseProperty("IdcouleurNavigation")]
    public virtual ICollection<Coloration> Colorations { get; set; } = new List<Coloration>();
}
