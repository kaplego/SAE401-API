using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.DTO;

public partial class ProfessionelDTO
{
    [Required]
    public int Idclient { get; set; }

    [Required]
    public int Idactivitepro { get; set; }

    [Required]
    public string Nomsociete { get; set; } = null!;

    [Required]
    public string Numtva { get; set; } = null!;
}
