using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("produit")]
[Index("Idpays", Name = "paysorigine_fk")]
[Index("Idproduit", Name = "produit_pk", IsUnique = true)]
[Index("Idtypeproduit", Name = "produittypeproduit_fk")]
public partial class Produit
{
    [Key]
    [Column("idproduit")]
    public int Idproduit { get; set; }

    [Column("idtypeproduit")]
    public int Idtypeproduit { get; set; }

    [Column("idpays")]
    public int Idpays { get; set; }

    [Column("nomproduit")]
    [StringLength(256)]
    public string Nomproduit { get; set; } = null!;

    [Column("sourcenotice")]
    [StringLength(256)]
    public string? Sourcenotice { get; set; }

    [Column("sourceaspecttechnique")]
    [StringLength(256)]
    public string? Sourceaspecttechnique { get; set; }

    [Column("delailivraison")]
    public int Delailivraison { get; set; }

    [Column("coutlivraison")]
    [Precision(10, 2)]
    public decimal Coutlivraison { get; set; }

    [Column("nbpaiementmax")]
    public int Nbpaiementmax { get; set; }

    [InverseProperty("IdproduitNavigation")]
    public virtual ICollection<Avisproduit> Avisproduits { get; set; } = new List<Avisproduit>();

    [InverseProperty("IdproduitNavigation")]
    public virtual ICollection<Coloration> Colorations { get; set; } = new List<Coloration>();

    [InverseProperty("IdproduitNavigation")]
    public virtual ICollection<Historiqueconsultation> Historiqueconsultations { get; set; } = new List<Historiqueconsultation>();

    [ForeignKey("Idpays")]
    [InverseProperty("Produits")]
    public virtual Pay IdpaysNavigation { get; set; } = null!;

    [ForeignKey("Idtypeproduit")]
    [InverseProperty("Produits")]
    public virtual Typeproduit IdtypeproduitNavigation { get; set; } = null!;

    [InverseProperty("IdproduitNavigation")]
    public virtual ICollection<Valeurattribut> Valeurattributs { get; set; } = new List<Valeurattribut>();

    [ForeignKey("Idproduit")]
    [InverseProperty("Idproduits")]
    public virtual ICollection<Client> Idclients { get; set; } = new List<Client>();

    [ForeignKey("Idproduit")]
    [InverseProperty("Idproduits")]
    public virtual ICollection<Produit> Idproduit2s { get; set; } = new List<Produit>();

    [ForeignKey("Idproduit2")]
    [InverseProperty("Idproduit2s")]
    public virtual ICollection<Produit> Idproduits { get; set; } = new List<Produit>();
}
