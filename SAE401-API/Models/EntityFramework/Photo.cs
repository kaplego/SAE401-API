using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("photo")]
[Index("Idphoto", Name = "photo_pk", IsUnique = true)]
public partial class Photo
{
    [Key]
    [Column("idphoto")]
    public int Idphoto { get; set; }

    [Column("sourcephoto")]
    [StringLength(256)]
    public string Sourcephoto { get; set; } = null!;

    [Column("descriptionphoto")]
    [StringLength(256)]
    public string? Descriptionphoto { get; set; }

    [InverseProperty("IdphotoNavigation")]
    public virtual ICollection<Categorieproduit> Categorieproduits { get; set; } = new List<Categorieproduit>();

    [ForeignKey("Idphoto")]
    [InverseProperty("Idphotos")]
    public virtual ICollection<Coloration> Colorations { get; set; } = new List<Coloration>();

    [ForeignKey("Idphoto")]
    [InverseProperty("Idphotos")]
    public virtual ICollection<Avisproduit> Idavis { get; set; } = new List<Avisproduit>();
}
