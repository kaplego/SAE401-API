using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_h_professionel_pro")]
public partial class Professionel
{
    [Key]
    [Column("pro_idclient")]
    public int Idclient { get; set; }

    [Column("pro_idactivitepro")]
    public int Idactivitepro { get; set; }

    [Column("pro_nomsociete")]
    public int Nomsociete { get; set; }

    [Column("pro_numtva")]
    [StringLength(11)]
    [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Le numéro de TVA doit contenir 11 chiffres.")]
    public string Numtva { get; set; } = null!;

    [ForeignKey(nameof(Idactivitepro))]
    [InverseProperty("Professionels")]
    public virtual Activitepro IdactiviteproNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idclient))]
    [InverseProperty("Professionel")]
    public virtual Client IdclientNavigation { get; set; } = null!;
}
