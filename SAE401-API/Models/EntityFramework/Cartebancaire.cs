using Microsoft.EntityFrameworkCore;
using SAE401_API.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_cartebancaire_car")]
[Index(nameof(Idclient), nameof(Nomcartebancaire), Name = "ix_t_e_cartebancaire_car_idclient_nomcartebancaire", IsUnique = true)]
[Index(nameof(Idclient), nameof(Numcartebancaire), Name = "ix_t_e_cartebancaire_car_idclient_numcartebancaire", IsUnique = true)]

public partial class Cartebancaire
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("car_idcartebancaire")]
    public int Idcartebancaire { get; set; }

    [Column("car_idclient")]
    public int Idclient { get; set; }

    [Column("car_titulairecartebancaire")]
    [StringLength(256)]
    public string? Titulairecartebancaire { get; set; }


    [Column("car_nomcartebancaire")]
    [StringLength(32)]
    public string? Nomcartebancaire { get; set; }

    [Column("car_dateenregistrement")]
    [DateValidator]
    [Required]
    public DateTime Dateenregistement { get; set; } = DateTime.UtcNow;

    [Column("car_numcartebancaire")]
    [StringLength(16)]
    [RegularExpression(@"^[0-9]{16}$", ErrorMessage = "Le numéro de carte doit contenir 16 chiffres.")]

    public string Numcartebancaire { get; set; } = null!;

    [Column("car_dateexpirationcarte")]
    [Required]
    [FutureDateValidator]
    public DateTime Dateexpirationcarte { get; set; }

    [ForeignKey(nameof(Idclient))]
    [InverseProperty(nameof(Client.CartesNavigation))]
    public virtual Client ClientNavigation { get; set; } = null!;

    [InverseProperty(nameof(Paiement.CarteNavigation))]
    public virtual ICollection<Paiement> PaiementsNavigation { get; set; } = new List<Paiement>();
}
