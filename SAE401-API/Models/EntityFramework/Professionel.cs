using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("professionel")]
[Index("Idactivitepro", Name = "proactivitepro_fk")]
[Index("Idclient", Name = "professionel_pk", IsUnique = true)]
public partial class Professionel
{
    [Key]
    [Column("idclient")]
    public int Idclient { get; set; }

    [Column("idactivitepro")]
    public int Idactivitepro { get; set; }

    [Column("nomsociete")]
    public int Nomsociete { get; set; }

    [Column("numtva")]
    [StringLength(11)]
    public string Numtva { get; set; } = null!;

    [ForeignKey("Idactivitepro")]
    [InverseProperty("Professionels")]
    public virtual Activitepro IdactiviteproNavigation { get; set; } = null!;

    [ForeignKey("Idclient")]
    [InverseProperty("Professionel")]
    public virtual Client IdclientNavigation { get; set; } = null!;
}
