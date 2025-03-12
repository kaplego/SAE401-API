using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey("Idproduit", "Idcouleur", "Idcommande")]
[Table("detailcommande")]
[Index("Idcommande", Name = "detailcommande2_fk")]
[Index("Idproduit", "Idcouleur", Name = "detailcommande_fk")]
[Index("Idproduit", "Idcouleur", "Idcommande", Name = "detailcommande_pk", IsUnique = true)]
public partial class Detailcommande
{
    [Key]
    [Column("idproduit")]
    public int Idproduit { get; set; }

    [Key]
    [Column("idcouleur")]
    public int Idcouleur { get; set; }

    [Key]
    [Column("idcommande")]
    public int Idcommande { get; set; }

    [Column("quantitecommande")]
    public int Quantitecommande { get; set; }

    [ForeignKey("Idproduit, Idcouleur")]
    [InverseProperty("Detailcommandes")]
    public virtual Coloration Coloration { get; set; } = null!;

    [ForeignKey("Idcommande")]
    [InverseProperty("Detailcommandes")]
    public virtual Commande IdcommandeNavigation { get; set; } = null!;
}
