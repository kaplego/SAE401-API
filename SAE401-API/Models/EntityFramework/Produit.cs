using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API.Models.EntityFramework;

[Table("t_e_produit_prd")]
[Index(nameof(Nomproduit), Name = "ix_t_e_produit_prd_nomproduit", IsUnique = true)]

public partial class Produit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("prd_idproduit")]
    public int Idproduit { get; set; }

    [Column("prd_idtypeproduit")]
    public int Idtypeproduit { get; set; }

    [Column("prd_idpays")]
    public int Idpays { get; set; }

    [Column("prd_nomproduit")]
    [StringLength(256)]
    public string Nomproduit { get; set; } = null!;

    [Column("prd_notice")]
    [StringLength(1024)]
    public string? Notice { get; set; }

    [Column("prd_aspecttechnique")]
    [StringLength(1024)]
    public string? Aspecttechnique { get; set; }

    [Column("prd_delailivraison")]
    public int Delailivraison { get; set; }

    [Column("prd_coutlivraison", TypeName = "numeric(10, 2)")]
    [Range(0, double.MaxValue, ErrorMessage = "La valeur doit être supérieure ou égale à 0.")]
    public decimal Coutlivraison { get; set; }

    [Column("prd_nbpaiementmax")]
    [Range(1, int.MaxValue, ErrorMessage = "La valeur doit être supérieure ou égale à 1.")]
    public int Nbpaiementmax { get; set; }

    [InverseProperty(nameof(Avisproduit.ProduitNavigation))]
    public virtual ICollection<Avisproduit> AvisNavigation { get; set; } = new List<Avisproduit>();

    [InverseProperty(nameof(Coloration.ProduitNavigation))]
    public virtual ICollection<Coloration> ColorationsNavigation { get; set; } = new List<Coloration>();

    [InverseProperty(nameof(Historiqueconsultation.ProduitNavigation))]
    public virtual ICollection<Historiqueconsultation> HistoriquesNavigation { get; set; } = new List<Historiqueconsultation>();

    [ForeignKey(nameof(Idpays))]
    [InverseProperty(nameof(Pay.ProduitsNavigation))]
    public virtual Pay PayNavigation { get; set; } = null!;

    [ForeignKey(nameof(Idtypeproduit))]
    [InverseProperty(nameof(Typeproduit.ProduitsNavigation))]
    public virtual Typeproduit TypeNavigation { get; set; } = null!;

    [InverseProperty(nameof(Valeurattribut.ProduitNavigation))]
    public virtual ICollection<Valeurattribut> ValeursNavigation { get; set; } = new List<Valeurattribut>();

    [InverseProperty(nameof(Produitsimilaire.ProduitRefNavigation))]
    public virtual ICollection<Produitsimilaire> SimilaireRefNavigation { get; set; } = new List<Produitsimilaire>();

    [InverseProperty(nameof(Produitsimilaire.ProduitSimNavigation))]
    public virtual ICollection<Produitsimilaire> SimilaireSimNavigation { get; set; } = new List<Produitsimilaire>();

    [InverseProperty(nameof(Aime.ProduitNavigation))]
    public virtual ICollection<Aime> AimesNavigation { get; set; } = new List<Aime>();
}
