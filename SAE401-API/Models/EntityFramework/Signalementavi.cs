using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Validation;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_signalementavis_sga")]
[Index(nameof(Idavis),nameof(Emailsignalement), Name = "ix_t_e_signalementavis_sga_idavis_emailsignalement", IsUnique = true)]

public partial class Signalementavi
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("sga_idsignalement")]
    public int Idsignalement { get; set; }

    [Column("sga_idavis")]
    public int Idavis { get; set; }

    [Column("sga_idtypesignalement")]
    public int Idtypesignalement { get; set; }

    [Column("sga_emailsignalement")]
    [StringLength(256)]
    [EmailAddress]
    public string Emailsignalement { get; set; } = null!;

    [Column("sga_datesignalement")]
    [DateValidator]
    [Required] 
    public DateTime Datesignalement { get; set; } = DateTime.UtcNow;

    [Column("sga_contenusignalement")]
    [StringLength(512)]
    public string Contenusignalement { get; set; } = null!;

    [ForeignKey(nameof(Idavis))]
    [InverseProperty(nameof(Avisproduit.SignalementsNavigation))]
    public virtual Avisproduit AviNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idtypesignalement))]
    [InverseProperty(nameof(Typesignalement.SignalementsNavigation))]
    public virtual Typesignalement TypeNavigation { get; set; } = null!;
}
