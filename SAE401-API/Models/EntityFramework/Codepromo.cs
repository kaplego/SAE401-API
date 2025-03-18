using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Validation;

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
    [FutureDateValidator]
    public DateTime? Dateexpirationcode { get; set; }

    [InverseProperty(nameof(Commande.CodeNavigation))]
    public virtual ICollection<Commande> CommandesNavigation { get; set; } = new List<Commande>();

    [ForeignKey(nameof(Idclient))]
    [InverseProperty(nameof(Client.CodesNavigation))]
    public virtual Client? ClientNavigation { get; set; }
}
