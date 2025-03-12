using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_activitepro_act")]
[Index(nameof(Nomactivitepro), Name = "ix_t_e_activitepro_act_nomactivitepro", IsUnique = true)]
public partial class Activitepro
{
    [Key]
    [Column("act_idactivitepro")]
    public int Idactivitepro { get; set; }

    [Column("act_nomactivitepro")]
    [StringLength(64)]
    public string Nomactivitepro { get; set; } = null!;

    [InverseProperty("IdactiviteproNavigation")]
    public virtual ICollection<Professionel> Professionels { get; set; } = new List<Professionel>();
}
