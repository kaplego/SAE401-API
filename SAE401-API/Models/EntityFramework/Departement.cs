using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_departement_dep")]
public partial class Departement
{
    [Key]
    [Column("dep_iddepartement")]
    public int Iddepartement { get; set; }

    [Column("dep_nomdepartement")]
    [StringLength(50)]
    public string? Nomdepartement { get; set; }

    [InverseProperty(nameof(Adresse.DepartementNavigation))]
    [JsonIgnore]
    public virtual ICollection<Adresse> AdressesNavigation { get; set; } = new List<Adresse>();
}
