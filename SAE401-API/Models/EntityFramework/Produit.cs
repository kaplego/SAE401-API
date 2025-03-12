using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_produit_prd")]
public partial class Produit
{
    [Key]
    [Column("prd_idproduit")]
    public int Idproduit { get; set; }

    [Column("prd_idtypeproduit")]
    public int Idtypeproduit { get; set; }

    [Column("prd_idpays")]
    public int Idpays { get; set; }

    [Column("prd_nomproduit")]
    [StringLength(256)]
    public string Nomproduit { get; set; } = null!;

    [Column("prd_sourcenotice")]
    [StringLength(256)]
    public string? Sourcenotice { get; set; }

    [Column("prd_sourceaspecttechnique")]
    [StringLength(256)]
    public string? Sourceaspecttechnique { get; set; }

    [Column("prd_delailivraison")]
    public int Delailivraison { get; set; }

    [Column("prd_coutlivraison")]
    [Precision(10, 2)]
    public decimal Coutlivraison { get; set; }

    [Column("prd_nbpaiementmax")]
    public int Nbpaiementmax { get; set; }

    [InverseProperty("IdproduitNavigation")]
    public virtual ICollection<Avisproduit> Avisproduits { get; set; } = new List<Avisproduit>();

    [InverseProperty("IdproduitNavigation")]
    public virtual ICollection<Coloration> Colorations { get; set; } = new List<Coloration>();

    [InverseProperty("IdproduitNavigation")]
    public virtual ICollection<Historiqueconsultation> Historiqueconsultations { get; set; } = new List<Historiqueconsultation>();

    [ForeignKey(nameof(Idpays))]
    [InverseProperty("Produits")]
    public virtual Pay IdpaysNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idtypeproduit))]
    [InverseProperty("Produits")]
    public virtual Typeproduit IdtypeproduitNavigation { get; set; } = null!;

    [InverseProperty("IdproduitNavigation")]
    public virtual ICollection<Valeurattribut> Valeurattributs { get; set; } = new List<Valeurattribut>();

    [ForeignKey(nameof(Idproduit))]
    [InverseProperty("Idproduits")]
    public virtual ICollection<Client> Idclients { get; set; } = new List<Client>();

    [ForeignKey(nameof(Idproduitsimilaire2))]
    [InverseProperty("Idproduitsimilaire2")]
    public virtual ICollection<Produit> Idproduitsimilaire { get; set; } = new List<Produit>();

    [ForeignKey(nameof(Idproduitsimilaire))]
    [InverseProperty("Idproduitsimilaire")]
    public virtual ICollection<Produit> Idproduitsimilaire2 { get; set; } = new List<Produit>();
}
