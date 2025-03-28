using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.DTO;

public partial class PhotoDTO
{
    public int? Idphoto { get; set; }

    [Required]
    public string Sourcephoto { get; set; } = null!;

    public string? Descriptionphoto { get; set; }

}
