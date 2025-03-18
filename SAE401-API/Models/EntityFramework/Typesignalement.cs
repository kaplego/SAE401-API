using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_typesignalement_tsg")]
[Index(nameof(Nomtypesignalement), Name = "ix_t_e_typesignalement_tsg_nomtypesignalement", IsUnique = true)]

public partial class Typesignalement
{
    [Key]
    [Column("tsg_idtypesignalement")]
    public int Idtypesignalement { get; set; }

    [Column("tsg_nomtypesignalement")]
    [StringLength(64)]
    public string Nomtypesignalement { get; set; } = null!;

    [InverseProperty(nameof(Signalementavi.TypeNavigation))]
    public virtual ICollection<Signalementavi> SignalementsNavigation { get; set; } = new List<Signalementavi>();
}
