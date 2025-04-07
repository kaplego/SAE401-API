using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SAE401_API.Validation;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey(nameof(Idclient), nameof(Idproduit))]
[Table("t_j_historiqueconsultation_hst")]
public partial class Historiqueconsultation
{
    [Key]
    [Column("hst_idclient")]
    public int Idclient { get; set; }

    [Key]
    [Column("hst_idproduit")]
    public int Idproduit { get; set; }

    [Column("hst_dateconsultation")]
    [DateValidator]
    [Required]
    public DateTime Dateconsultation { get; set; }= DateTime.UtcNow;

    [ForeignKey(nameof(Idclient))]
    [InverseProperty(nameof(Client.HistoriquesNavigation))]
    public virtual Client ClientNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idproduit))]
    [InverseProperty(nameof(Produit.HistoriquesNavigation))]
    public virtual Produit ProduitNavigation { get; set; } = null!;
}
