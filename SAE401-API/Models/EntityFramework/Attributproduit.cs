using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_attributproduit_att")]
[Index(nameof(Idtypeproduit), nameof(Nomattribut), Name = "ix_t_e_attributproduit_att_idtypeproduit_nomattribut", IsUnique = true)]

public partial class Attributproduit
{
    [Key]
    [Column("att_idattribut")]
    public int Idattribut { get; set; }

    [Column("att_idtypeproduit")]
    public int Idtypeproduit { get; set; }

    [Column("att_nomattribut")]
    [StringLength(64)]
    public string Nomattribut { get; set; } = null!;

    [ForeignKey(nameof(Idtypeproduit))]
    [InverseProperty(nameof(Typeproduit.AttributsNavigation))]
    public virtual Typeproduit TypeproduitNavigation { get; set; } = null!;

    [InverseProperty(nameof(Valeurattribut.AttributNavigation))]
    public virtual ICollection<Valeurattribut> ValeursNavigation { get; set; } = new List<Valeurattribut>();
}
