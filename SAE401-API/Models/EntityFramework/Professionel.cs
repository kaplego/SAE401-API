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
    [StringLength(256)]
    public string Nomsociete { get; set; } = null!;

    [Column("pro_numtva")]
    [StringLength(11)]
    [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Le numéro de TVA doit contenir 11 chiffres.")]
    public string Numtva { get; set; } = null!;

    [ForeignKey(nameof(Idactivitepro))]
    [InverseProperty(nameof(Activitepro.ProfessionelsNavigation))]
    public virtual Activitepro ActiviteproNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idclient))]
    [InverseProperty(nameof(Client.ProfessionelNavigation))]
    public virtual Client ClientNavigation { get; set; } = null!;
}
