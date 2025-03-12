using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey("Idcomposition", "Idcommande")]
[Table("commandecomposition")]
[Index("Idcommande", Name = "commandecomposition2_fk")]
[Index("Idcomposition", Name = "commandecomposition_fk")]
[Index("Idcomposition", "Idcommande", Name = "commandecomposition_pk", IsUnique = true)]
public partial class Commandecomposition
{
    [Key]
    [Column("idcomposition")]
    public int Idcomposition { get; set; }

    [Key]
    [Column("idcommande")]
    public int Idcommande { get; set; }

    [Column("quantitecompositioncommande")]
    public int Quantitecompositioncommande { get; set; }

    [ForeignKey("Idcommande")]
    [InverseProperty("Commandecompositions")]
    public virtual Commande IdcommandeNavigation { get; set; } = null!;

    [ForeignKey("Idcomposition")]
    [InverseProperty("Commandecompositions")]
    public virtual Compositionproduit IdcompositionNavigation { get; set; } = null!;
}
