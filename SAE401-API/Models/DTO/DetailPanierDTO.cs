using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.DTO;

public partial class DetailpanierDTO
{
    public int Idproduit { get; set; }

    public int Idcouleur { get; set; }

    public int Idclient { get; set; }

    public int Quantitepanier { get; set; }
}
