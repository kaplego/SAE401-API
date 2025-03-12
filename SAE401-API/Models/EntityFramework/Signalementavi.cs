using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("signalementavis")]
[Index("Idavis", Name = "avissignalementavis_fk")]
[Index("Idsignalement", Name = "signalementavis_pk", IsUnique = true)]
[Index("Idtypesignalement", Name = "signalementtypesignalement_fk")]
public partial class Signalementavi
{
    [Key]
    [Column("idsignalement")]
    public int Idsignalement { get; set; }

    [Column("idavis")]
    public int Idavis { get; set; }

    [Column("idtypesignalement")]
    public int Idtypesignalement { get; set; }

    [Column("emailsignalement")]
    [StringLength(256)]
    public string Emailsignalement { get; set; } = null!;

    [Column("datesignalement")]
    public DateOnly Datesignalement { get; set; }

    [Column("contenusignalement")]
    [StringLength(512)]
    public string Contenusignalement { get; set; } = null!;

    [ForeignKey("Idavis")]
    [InverseProperty("Signalementavis")]
    public virtual Avisproduit IdavisNavigation { get; set; } = null!;

    [ForeignKey("Idtypesignalement")]
    [InverseProperty("Signalementavis")]
    public virtual Typesignalement IdtypesignalementNavigation { get; set; } = null!;
}
