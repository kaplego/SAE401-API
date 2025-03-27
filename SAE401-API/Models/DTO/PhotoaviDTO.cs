using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.DTO;

public partial class PhotoaviDTO
{
    [Required]
    public int Idavis { get; set; }

    [Required]
    public int Idphoto { get; set; }
}
