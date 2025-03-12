using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey("Idproduit", "Idcouleur", "Idcomposition")]
[Table("detailcomposition")]
[Index("Idcomposition", Name = "detailcomposition2_fk")]
[Index("Idproduit", "Idcouleur", Name = "detailcomposition_fk")]
[Index("Idproduit", "Idcouleur", "Idcomposition", Name = "detailcomposition_pk", IsUnique = true)]
public partial class Detailcomposition
{
    [Key]
    [Column("idproduit")]
    public int Idproduit { get; set; }

    [Key]
    [Column("idcouleur")]
    public int Idcouleur { get; set; }

    [Key]
    [Column("idcomposition")]
    public int Idcomposition { get; set; }

    [Column("quantitecomposition")]
    public int Quantitecomposition { get; set; }

    [ForeignKey("Idproduit, Idcouleur")]
    [InverseProperty("Detailcompositions")]
    public virtual Coloration Coloration { get; set; } = null!;

    [ForeignKey("Idcomposition")]
    [InverseProperty("Detailcompositions")]
    public virtual Compositionproduit IdcompositionNavigation { get; set; } = null!;
}
