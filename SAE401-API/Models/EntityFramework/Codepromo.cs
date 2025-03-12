using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_codepromo_cod")]
public partial class Codepromo
{
    [Key]
    [Column("cod_idcodepromo")]
    public int Idcodepromo { get; set; }

    [Column("cod_idclient")]
    public int? Idclient { get; set; }

    [Column("cod_nomcodepromo")]
    [StringLength(16)]
    public string Nomcodepromo { get; set; } = null!;

    [Column("cod_valeurreduction", TypeName = "numeric(5, 2)")]
    [Range(0.0001, double.MaxValue, ErrorMessage = "La valeur doit être strictement supérieure à 0.")]
    public decimal Valeurreduction { get; set; }

    [Column("cod_estvalide")]
    public bool Estvalide { get; set; }

    [Column("cod_dateexpirationcode")]
    public DateTime? Dateexpirationcode { get; set; }

    [InverseProperty("IdcodepromoNavigation")]
    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    [ForeignKey(nameof(Idclient))]
    [InverseProperty("Codepromos")]
    public virtual Client? IdclientNavigation { get; set; }
}
