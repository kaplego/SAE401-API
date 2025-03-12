using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_photo_pho")]
public partial class Photo
{
    [Key]
    [Column("pho_idphoto")]
    public int Idphoto { get; set; }

    [Column("pho_sourcephoto")]
    [StringLength(256)]
    public string Sourcephoto { get; set; } = null!;

    [Column("pho_descriptionphoto")]
    [StringLength(256)]
    public string? Descriptionphoto { get; set; }

    [InverseProperty("IdphotoNavigation")]
    public virtual ICollection<Categorieproduit> Categorieproduits { get; set; } = new List<Categorieproduit>();

    [ForeignKey(nameof(Idphoto))]
    [InverseProperty("Idphotos")]
    public virtual ICollection<Coloration> Colorations { get; set; } = new List<Coloration>();

    [ForeignKey(nameof(Idphoto))]
    [InverseProperty("Idphotos")]
    public virtual ICollection<Avisproduit> Idavis { get; set; } = new List<Avisproduit>();
}
