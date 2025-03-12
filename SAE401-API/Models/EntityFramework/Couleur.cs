using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_couleur_cou")]
public partial class Couleur
{
    [Key]
    [Column("cou_idcouleur")]
    public int Idcouleur { get; set; }

    [Column("cou_nomcouleur")]
    [StringLength(64)]
    public string Nomcouleur { get; set; } = null!;

    [Column("cou_rgbcouleur")]
    [StringLength(6)]
    public string Rgbcouleur { get; set; } = null!;

    [InverseProperty("IdcouleurNavigation")]
    public virtual ICollection<Coloration> Colorations { get; set; } = new List<Coloration>();
}
