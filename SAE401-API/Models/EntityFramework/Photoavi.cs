using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idavis), nameof(Idphoto))]
[Table("t_j_photoavis_pav")]
public partial class Photoavi
{
    [Key]
    [Column("pav_idavis")]
    public int Idavis { get; set; }

    [Key]
    [Column("pav_idproduit")]
    public int Idphoto { get; set; }

    [ForeignKey(nameof(Idavis))]
    [InverseProperty(nameof(Avisproduit.PhotoavisNavigation))]
    public virtual Avisproduit AviNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idphoto))]
    [InverseProperty(nameof(Photo.PhotocolsNavigation))]
    public virtual Photo PhotoNavigation { get; set; } = null!;
}
