using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_attributproduit_att")]
[Index(nameof(Idtypeproduit),nameof(Nomattribut), Name = "ix_t_e_attributproduit_att_idtypeproduit_nomattribut", IsUnique = true)]

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
    [InverseProperty("Attributproduits")]
    public virtual Typeproduit IdtypeproduitNavigation { get; set; } = null!;

    [InverseProperty("IdattributNavigation")]
    public virtual ICollection<Valeurattribut> Valeurattributs { get; set; } = new List<Valeurattribut>();
}
