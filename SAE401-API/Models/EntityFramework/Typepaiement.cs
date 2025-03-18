using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_typepaiement_tpm")]
[Index(nameof(Nomtypepaiement), Name = "ix_t_e_typepaiement_tpm_nomtypepaiement", IsUnique = true)]

public partial class Typepaiement
{
    [Key]
    [Column("tpm_idtypepaiement")]
    public int Idtypepaiement { get; set; }

    [Column("tpm_nomtypepaiement")]
    [StringLength(64)]
    public string Nomtypepaiement { get; set; } = null!;

    [InverseProperty(nameof(Paiement.TypeNavigation))]
    public virtual ICollection<Paiement> PaiementsNavigation { get; set; } = new List<Paiement>();
}
