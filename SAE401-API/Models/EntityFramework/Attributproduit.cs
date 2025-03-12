using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("attributproduit")]
[Index("Idattribut", Name = "attributproduit_pk", IsUnique = true)]
[Index("Idtypeproduit", Name = "attributtypeproduit_fk")]
public partial class Attributproduit
{
    [Key]
    [Column("idattribut")]
    public int Idattribut { get; set; }

    [Column("idtypeproduit")]
    public int Idtypeproduit { get; set; }

    [Column("nomattribut")]
    [StringLength(64)]
    public string Nomattribut { get; set; } = null!;

    [ForeignKey("Idtypeproduit")]
    [InverseProperty("Attributproduits")]
    public virtual Typeproduit IdtypeproduitNavigation { get; set; } = null!;

    [InverseProperty("IdattributNavigation")]
    public virtual ICollection<Valeurattribut> Valeurattributs { get; set; } = new List<Valeurattribut>();
}
