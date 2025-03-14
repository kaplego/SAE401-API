using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idclient), nameof(Idproduit))]
[Table("t_j_aime_aim")]
public partial class Aime
{
    [Key]
    [Column("aim_idclient")]
    public int Idclient { get; set; }

    [Key]
    [Column("aim_idproduit")]
    public int Idproduit { get; set; }

    [ForeignKey(nameof(Idclient))]
    [InverseProperty("Aimes")]
    public virtual Client IdclientNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idproduit))]
    [InverseProperty("Aimes")]
    public virtual Produit IdproduitNavigation { get; set; } = null!;
}
