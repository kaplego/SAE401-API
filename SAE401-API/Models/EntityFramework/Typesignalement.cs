using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_typesignalement_tsg")]
public partial class Typesignalement
{
    [Key]
    [Column("tsg_idtypesignalement")]
    public int Idtypesignalement { get; set; }

    [Column("tsg_nomtypesignalement")]
    [StringLength(64)]
    public string Nomtypesignalement { get; set; } = null!;

    [InverseProperty("IdtypesignalementNavigation")]
    public virtual ICollection<Signalementavi> Signalementavis { get; set; } = new List<Signalementavi>();
}
