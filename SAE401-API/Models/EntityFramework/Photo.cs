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

    [InverseProperty(nameof(Categorieproduit.PhotoNavigation))]
    public virtual ICollection<Categorieproduit> CategoriesNavigation { get; set; } = new List<Categorieproduit>();

    [InverseProperty(nameof(Photoavi.PhotoNavigation))]
    public virtual ICollection<Photoavi> PhotoavisNavigation { get; set; } = null!;

    [InverseProperty(nameof(Photocoloration.PhotoNavigation))]
    public virtual ICollection<Photocoloration> PhotocolsNavigation { get; set; } = null!;
}
