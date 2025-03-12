using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Validation;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_cartebancaire_car")]
[Index(nameof(Idclient),nameof(Nomcartebancaire), Name = "ix_t_e_cartebancaire_car_idclient_nomcartebancaire", IsUnique = true)]
[Index(nameof(Idclient), nameof(Numcartebancaire), Name = "ix_t_e_cartebancaire_car_idclient_numcartebancaire", IsUnique = true)]

public partial class Cartebancaire
{
    [Key]
    [Column("car_idcartebancaire")]
    public int Idcartebancaire { get; set; }

    [Column("car_idclient")]
    public int Idclient { get; set; }

    [Column("car_nomcartebancaire")]
    [StringLength(32)]
    public string? Nomcartebancaire { get; set; }

    [Column("car_dateenregistement")]
    [DateValidator]
    [Required]
    public DateTime Dateenregistement { get; set; }= DateTime.Now;

    [Column("car_numcartebancaire")]
    [StringLength(16)]
    [RegularExpression(@"^[0-9]{16}$", ErrorMessage = "Le numéro de carte doit contenir 16 chiffres.")]

    public string Numcartebancaire { get; set; } = null!;

    [Column("car_dateexpirationcarte")]
    [Required]
    [FutureDateValidator]
    public DateTime Dateexpirationcarte { get; set; }

    [ForeignKey(nameof(Idclient))]
    [InverseProperty("Cartebancaires")]
    public virtual Client IdclientNavigation { get; set; } = null!;

    [InverseProperty("IdcartebancaireNavigation")]
    public virtual ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();
}
