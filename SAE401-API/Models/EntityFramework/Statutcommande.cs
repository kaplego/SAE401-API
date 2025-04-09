using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_statutcommande_scd")]
[Index(nameof(Nomstatut), Name = "ix_t_e_statutcommande_scd_nomstatut", IsUnique = true)]

public partial class Statutcommande
{
    [Key]
    [Column("scd_idstatut")]
    public int Idstatut { get; set; }

    [Column("scd_nomstatut")]
    [StringLength(64)]
    public string Nomstatut { get; set; } = null!;

    [InverseProperty(nameof(Commande.StatutNavigation))]
    public virtual ICollection<Commande> CommandesNavigation { get; set; } = new List<Commande>();
}
