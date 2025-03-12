using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey("Idproduit", "Idcouleur", "Idclient")]
[Table("detailpanier")]
[Index("Idproduit", "Idcouleur", Name = "detailpanier2_fk")]
[Index("Idclient", Name = "detailpanier_fk")]
[Index("Idproduit", "Idcouleur", "Idclient", Name = "detailpanier_pk", IsUnique = true)]
public partial class Detailpanier
{
    [Key]
    [Column("idproduit")]
    public int Idproduit { get; set; }

    [Key]
    [Column("idcouleur")]
    public int Idcouleur { get; set; }

    [Key]
    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("quantitepanier")]
    public int Quantitepanier { get; set; }

    [ForeignKey("Idproduit, Idcouleur")]
    [InverseProperty("Detailpaniers")]
    public virtual Coloration Coloration { get; set; } = null!;

    [ForeignKey("Idclient")]
    [InverseProperty("Detailpaniers")]
    public virtual Client IdclientNavigation { get; set; } = null!;
}
