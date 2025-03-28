using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Validation;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_paiement_pmt")]
public partial class Paiement
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("pmt_idpaiement")]
    public int Idpaiement { get; set; }

    [Column("pmt_idcartebancaire")]
    public int? Idcartebancaire { get; set; }

    [Column("pmt_idcommande")]
    public int Idcommande { get; set; }

    [Column("pmt_idtypepaiement")]
    public int Idtypepaiement { get; set; }

    [Column("pmt_datepaiement")]
    [DateValidator]
    [Required]
    public DateTime Datepaiement { get; set; } = DateTime.Now;

    [Column("pmt_montantpaiement", TypeName = "numeric(10, 2)")]
    [Range(0.0001, double.MaxValue, ErrorMessage = "La valeur doit être strictement supérieure à 0.")]
    public decimal Montantpaiement { get; set; }

    [Column("pmt_indicepaiement")]
    [StringLength(256)]
    public string? Indicepaiement { get; set; }

    [ForeignKey(nameof(Idcartebancaire))]
    [InverseProperty(nameof(Cartebancaire.PaiementsNavigation))]
    public virtual Cartebancaire? CarteNavigation { get; set; }

    [ForeignKey(nameof(Idcommande))]
    [InverseProperty(nameof(Commande.PaiementsNavigation))]
    public virtual Commande CommandeNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idtypepaiement))]
    [InverseProperty(nameof(Typepaiement.PaiementsNavigation))]
    public virtual Typepaiement TypeNavigation { get; set; } = null!;
}
