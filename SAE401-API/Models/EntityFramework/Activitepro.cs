using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("activitepro")]
[Index("Idactivitepro", Name = "activitepro_pk", IsUnique = true)]
public partial class Activitepro
{
    [Key]
    [Column("idactivitepro")]
    public int Idactivitepro { get; set; }

    [Column("nomactivitepro")]
    [StringLength(64)]
    public string Nomactivitepro { get; set; } = null!;

    [InverseProperty("IdactiviteproNavigation")]
    public virtual ICollection<Professionel> Professionels { get; set; } = new List<Professionel>();
}
