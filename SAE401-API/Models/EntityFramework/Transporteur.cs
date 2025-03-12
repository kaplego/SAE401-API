using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_transporteur_tpt")]
public partial class Transporteur
{
    [Key]
    [Column("tpt_idtransporteur")]
    public int Idtransporteur { get; set; }

    [Column("tpt_nomtransporteur")]
    [StringLength(64)]
    public string Nomtransporteur { get; set; } = null!;

    [InverseProperty("IdtransporteurNavigation")]
    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
}
