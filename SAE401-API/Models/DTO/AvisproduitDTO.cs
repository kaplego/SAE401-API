using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Validation;

namespace SAE401_API.Models.DTO;

public partial class AvisproduitDTO
{
    public int? Idavis { get; set; }

    [Required]
    public int Idproduit { get; set; }

    [Required]
    public int Idclient { get; set; }

    [Required]
    public int Noteavis { get; set; }

    [Required]
    public DateTime Dateavis { get; set; } = DateTime.Now;

    public string? Commentaireavis { get; set; }

    public string? Reponsemiliboo { get; set; }

}
