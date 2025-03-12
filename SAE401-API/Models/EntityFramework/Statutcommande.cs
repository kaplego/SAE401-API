using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("statutcommande")]
[Index("Idstatut", Name = "statutcommande_pk", IsUnique = true)]
public partial class Statutcommande
{
    [Key]
    [Column("idstatut")]
    public int Idstatut { get; set; }

    [Column("nomstatut")]
    [StringLength(64)]
    public string Nomstatut { get; set; } = null!;

    [InverseProperty("IdstatutNavigation")]
    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
}
