using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey("Idattribut", "Idproduit")]
[Table("valeurattribut")]
[Index("Idproduit", Name = "valeurattribut2_fk")]
[Index("Idattribut", Name = "valeurattribut_fk")]
[Index("Idattribut", "Idproduit", Name = "valeurattribut_pk", IsUnique = true)]
public partial class Valeurattribut
{
    [Key]
    [Column("idattribut")]
    public int Idattribut { get; set; }

    [Key]
    [Column("idproduit")]
    public int Idproduit { get; set; }

    [Column("valeur")]
    [StringLength(64)]
    public string Valeur { get; set; } = null!;

    [ForeignKey("Idattribut")]
    [InverseProperty("Valeurattributs")]
    public virtual Attributproduit IdattributNavigation { get; set; } = null!;

    [ForeignKey("Idproduit")]
    [InverseProperty("Valeurattributs")]
    public virtual Produit IdproduitNavigation { get; set; } = null!;
}
