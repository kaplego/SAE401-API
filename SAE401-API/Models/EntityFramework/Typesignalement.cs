using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("typesignalement")]
[Index("Idtypesignalement", Name = "typesignalement_pk", IsUnique = true)]
public partial class Typesignalement
{
    [Key]
    [Column("idtypesignalement")]
    public int Idtypesignalement { get; set; }

    [Column("nomtypesignalement")]
    [StringLength(64)]
    public string Nomtypesignalement { get; set; } = null!;

    [InverseProperty("IdtypesignalementNavigation")]
    public virtual ICollection<Signalementavi> Signalementavis { get; set; } = new List<Signalementavi>();
}
