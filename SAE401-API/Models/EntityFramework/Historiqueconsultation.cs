using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[PrimaryKey("Idclient", "Idproduit")]
[Table("historiqueconsultation")]
[Index("Idproduit", Name = "historiqueconsultation2_fk")]
[Index("Idclient", Name = "historiqueconsultation_fk")]
[Index("Idclient", "Idproduit", Name = "historiqueconsultation_pk", IsUnique = true)]
public partial class Historiqueconsultation
{
    [Key]
    [Column("idclient")]
    public int Idclient { get; set; }

    [Key]
    [Column("idproduit")]
    public int Idproduit { get; set; }

    [Column("dateconsultation")]
    public DateOnly Dateconsultation { get; set; }

    [ForeignKey("Idclient")]
    [InverseProperty("Historiqueconsultations")]
    public virtual Client IdclientNavigation { get; set; } = null!;

    [ForeignKey("Idproduit")]
    [InverseProperty("Historiqueconsultations")]
    public virtual Produit IdproduitNavigation { get; set; } = null!;
}
