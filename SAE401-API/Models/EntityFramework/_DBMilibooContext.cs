using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API.Models.EntityFramework;

public partial class _DBMilibooContext : DbContext
{
    public _DBMilibooContext()
    {
    }

    public _DBMilibooContext(DbContextOptions<_DBMilibooContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activitepro> Activitepros { get; set; }

    public virtual DbSet<Adresse> Adresses { get; set; }

    public virtual DbSet<Attributproduit> Attributproduits { get; set; }

    public virtual DbSet<Avisproduit> Avisproduits { get; set; }

    public virtual DbSet<Cartebancaire> Cartebancaires { get; set; }

    public virtual DbSet<Categorieproduit> Categorieproduits { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Codepromo> Codepromos { get; set; }

    public virtual DbSet<Coloration> Colorations { get; set; }

    public virtual DbSet<Commande> Commandes { get; set; }

    public virtual DbSet<Commandecomposition> Commandecompositions { get; set; }

    public virtual DbSet<Compositionproduit> Compositionproduits { get; set; }

    public virtual DbSet<Couleur> Couleurs { get; set; }

    public virtual DbSet<Departement> Departements { get; set; }

    public virtual DbSet<Detailcommande> Detailcommandes { get; set; }

    public virtual DbSet<Detailcomposition> Detailcompositions { get; set; }

    public virtual DbSet<Detailpanier> Detailpaniers { get; set; }

    public virtual DbSet<Historiqueconsultation> Historiqueconsultations { get; set; }

    public virtual DbSet<Messagechatbot> Messagechatbots { get; set; }

    public virtual DbSet<Paiement> Paiements { get; set; }

    public virtual DbSet<Pay> Pays { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Produit> Produits { get; set; }

    public virtual DbSet<Professionel> Professionels { get; set; }

    public virtual DbSet<Regroupementproduit> Regroupementproduits { get; set; }

    public virtual DbSet<Signalementavi> Signalementavis { get; set; }

    public virtual DbSet<Statutcommande> Statutcommandes { get; set; }

    public virtual DbSet<Transporteur> Transporteurs { get; set; }

    public virtual DbSet<Typepaiement> Typepaiements { get; set; }

    public virtual DbSet<Typeproduit> Typeproduits { get; set; }

    public virtual DbSet<Typesignalement> Typesignalements { get; set; }

    public virtual DbSet<Valeurattribut> Valeurattributs { get; set; }

    public virtual DbSet<Ville> Villes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=Test; uid=postgres; password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activitepro>(entity =>
        {
            entity.HasKey(e => e.Idactivitepro).HasName("pk_activitepro");
        });

        modelBuilder.Entity<Adresse>(entity =>
        {
            entity.HasKey(e => e.Idadresse).HasName("pk_adresse");

            entity.Property(e => e.Codeinsee).IsFixedLength();
            entity.Property(e => e.Codepostaladresse).IsFixedLength();

            entity.HasOne(d => d.CodeinseeNavigation).WithMany(p => p.Adresses)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adresse_residedan_ville");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Adresses)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adresse_adressecl_client");

            entity.HasOne(d => d.IddepartementNavigation).WithMany(p => p.Adresses)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adresse_estsitue_departem");

            entity.HasOne(d => d.IdpaysNavigation).WithMany(p => p.Adresses)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adresse_adressepa_pays");
        });

        modelBuilder.Entity<Attributproduit>(entity =>
        {
            entity.HasKey(e => e.Idattribut).HasName("pk_attributproduit");

            entity.HasOne(d => d.IdtypeproduitNavigation).WithMany(p => p.Attributproduits)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_attribut_attributt_typeprod");
        });

        modelBuilder.Entity<Avisproduit>(entity =>
        {
            entity.HasKey(e => e.Idavis).HasName("pk_avisproduit");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Avisproduits)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_avisprod_avisclien_client");

            entity.HasOne(d => d.IdproduitNavigation).WithMany(p => p.Avisproduits)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_avisprod_avispourp_produit");

            entity.HasMany(d => d.Idphotos).WithMany(p => p.Idavis)
                .UsingEntity<Dictionary<string, object>>(
                    "Photoavi",
                    r => r.HasOne<Photo>().WithMany()
                        .HasForeignKey("Idphoto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_photoavi_photoavis_photo"),
                    l => l.HasOne<Avisproduit>().WithMany()
                        .HasForeignKey("Idavis")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_photoavi_photoavis_avisprod"),
                    j =>
                    {
                        j.HasKey("Idavis", "Idphoto").HasName("pk_photoavis");
                        j.ToTable("photoavis");
                        j.HasIndex(new[] { "Idphoto" }, "photoavis2_fk");
                        j.HasIndex(new[] { "Idavis" }, "photoavis_fk");
                        j.HasIndex(new[] { "Idavis", "Idphoto" }, "photoavis_pk").IsUnique();
                        j.IndexerProperty<int>("Idavis").HasColumnName("idavis");
                        j.IndexerProperty<int>("Idphoto").HasColumnName("idphoto");
                    });
        });

        modelBuilder.Entity<Cartebancaire>(entity =>
        {
            entity.HasKey(e => e.Idcartebancaire).HasName("pk_cartebancaire");

            entity.Property(e => e.Numcartebancaire).IsFixedLength();

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Cartebancaires)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_carteban_carteclie_client");
        });

        modelBuilder.Entity<Categorieproduit>(entity =>
        {
            entity.HasKey(e => e.Idcategorie).HasName("pk_categorieproduit");

            entity.HasOne(d => d.CatIdcategorieNavigation).WithMany(p => p.InverseCatIdcategorieNavigation)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_categori_categorie_categori");

            entity.HasOne(d => d.IdphotoNavigation).WithMany(p => p.Categorieproduits)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_categori_photocate_photo");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Idclient).HasName("pk_client");

            entity.Property(e => e.Datecreationcompte).IsFixedLength();
            entity.Property(e => e.Hashmdp).IsFixedLength();
            entity.Property(e => e.Telfixeclient).IsFixedLength();
            entity.Property(e => e.Telportableclient).IsFixedLength();

            entity.HasMany(d => d.Idproduits).WithMany(p => p.Idclients)
                .UsingEntity<Dictionary<string, object>>(
                    "Aime",
                    r => r.HasOne<Produit>().WithMany()
                        .HasForeignKey("Idproduit")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_aime_aime2_produit"),
                    l => l.HasOne<Client>().WithMany()
                        .HasForeignKey("Idclient")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_aime_aime_client"),
                    j =>
                    {
                        j.HasKey("Idclient", "Idproduit").HasName("pk_aime");
                        j.ToTable("aime");
                        j.HasIndex(new[] { "Idproduit" }, "aime2_fk");
                        j.HasIndex(new[] { "Idclient" }, "aime_fk");
                        j.HasIndex(new[] { "Idclient", "Idproduit" }, "aime_pk").IsUnique();
                        j.IndexerProperty<int>("Idclient").HasColumnName("idclient");
                        j.IndexerProperty<int>("Idproduit").HasColumnName("idproduit");
                    });
        });

        modelBuilder.Entity<Codepromo>(entity =>
        {
            entity.HasKey(e => e.Idcodepromo).HasName("pk_codepromo");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Codepromos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_codeprom_clientcod_client");
        });

        modelBuilder.Entity<Coloration>(entity =>
        {
            entity.HasKey(e => new { e.Idproduit, e.Idcouleur }).HasName("pk_coloration");

            entity.HasOne(d => d.IdcouleurNavigation).WithMany(p => p.Colorations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_colorati_coloratio_couleur");

            entity.HasOne(d => d.IdproduitNavigation).WithMany(p => p.Colorations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_colorati_coloratio_produit");

            entity.HasMany(d => d.Idphotos).WithMany(p => p.Colorations)
                .UsingEntity<Dictionary<string, object>>(
                    "Photoproduitcoloration",
                    r => r.HasOne<Photo>().WithMany()
                        .HasForeignKey("Idphoto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_photopro_photoprod_photo"),
                    l => l.HasOne<Coloration>().WithMany()
                        .HasForeignKey("Idproduit", "Idcouleur")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_photopro_photoprod_colorati"),
                    j =>
                    {
                        j.HasKey("Idproduit", "Idcouleur", "Idphoto").HasName("pk_photoproduitcoloration");
                        j.ToTable("photoproduitcoloration");
                        j.HasIndex(new[] { "Idproduit", "Idcouleur" }, "photoproduitcoloration2_fk");
                        j.HasIndex(new[] { "Idphoto" }, "photoproduitcoloration_fk");
                        j.HasIndex(new[] { "Idproduit", "Idcouleur", "Idphoto" }, "photoproduitcoloration_pk").IsUnique();
                        j.IndexerProperty<int>("Idproduit").HasColumnName("idproduit");
                        j.IndexerProperty<int>("Idcouleur").HasColumnName("idcouleur");
                        j.IndexerProperty<int>("Idphoto").HasColumnName("idphoto");
                    });

            entity.HasMany(d => d.Idregroupements).WithMany(p => p.Colorations)
                .UsingEntity<Dictionary<string, object>>(
                    "Detailregroupement",
                    r => r.HasOne<Regroupementproduit>().WithMany()
                        .HasForeignKey("Idregroupement")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_detailre_detailreg_regroupe"),
                    l => l.HasOne<Coloration>().WithMany()
                        .HasForeignKey("Idproduit", "Idcouleur")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_detailre_detailreg_colorati"),
                    j =>
                    {
                        j.HasKey("Idproduit", "Idcouleur", "Idregroupement").HasName("pk_detailregroupement");
                        j.ToTable("detailregroupement");
                        j.HasIndex(new[] { "Idproduit", "Idcouleur" }, "detailregroupement2_fk");
                        j.HasIndex(new[] { "Idregroupement" }, "detailregroupement_fk");
                        j.HasIndex(new[] { "Idproduit", "Idcouleur", "Idregroupement" }, "detailregroupement_pk").IsUnique();
                        j.IndexerProperty<int>("Idproduit").HasColumnName("idproduit");
                        j.IndexerProperty<int>("Idcouleur").HasColumnName("idcouleur");
                        j.IndexerProperty<int>("Idregroupement").HasColumnName("idregroupement");
                    });
        });

        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasKey(e => e.Idcommande).HasName("pk_commande");

            entity.HasOne(d => d.AdrIdadresseNavigation).WithMany(p => p.CommandeAdrIdadresseNavigations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_adressefa_adresse");

            entity.HasOne(d => d.IdadresseNavigation).WithMany(p => p.CommandeIdadresseNavigations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_adresseli_adresse");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Commandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_clientcom_client");

            entity.HasOne(d => d.IdcodepromoNavigation).WithMany(p => p.Commandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_codepromo_codeprom");

            entity.HasOne(d => d.IdstatutNavigation).WithMany(p => p.Commandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_commandes_statutco");

            entity.HasOne(d => d.IdtransporteurNavigation).WithMany(p => p.Commandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_transport_transpor");
        });

        modelBuilder.Entity<Commandecomposition>(entity =>
        {
            entity.HasKey(e => new { e.Idcomposition, e.Idcommande }).HasName("pk_commandecomposition");

            entity.HasOne(d => d.IdcommandeNavigation).WithMany(p => p.Commandecompositions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_commandec_commande");

            entity.HasOne(d => d.IdcompositionNavigation).WithMany(p => p.Commandecompositions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_commandec_composit");
        });

        modelBuilder.Entity<Compositionproduit>(entity =>
        {
            entity.HasKey(e => e.Idcomposition).HasName("pk_compositionproduit");
        });

        modelBuilder.Entity<Couleur>(entity =>
        {
            entity.HasKey(e => e.Idcouleur).HasName("pk_couleur");

            entity.Property(e => e.Rgbcouleur).IsFixedLength();
        });

        modelBuilder.Entity<Departement>(entity =>
        {
            entity.HasKey(e => e.Iddepartement).HasName("pk_departement");
        });

        modelBuilder.Entity<Detailcommande>(entity =>
        {
            entity.HasKey(e => new { e.Idproduit, e.Idcouleur, e.Idcommande }).HasName("pk_detailcommande");

            entity.HasOne(d => d.IdcommandeNavigation).WithMany(p => p.Detailcommandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailco_detailcom_commande");

            entity.HasOne(d => d.Coloration).WithMany(p => p.Detailcommandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailco_detailcom_colorati");
        });

        modelBuilder.Entity<Detailcomposition>(entity =>
        {
            entity.HasKey(e => new { e.Idproduit, e.Idcouleur, e.Idcomposition }).HasName("pk_detailcomposition");

            entity.HasOne(d => d.IdcompositionNavigation).WithMany(p => p.Detailcompositions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailco_detailcom_composit");

            entity.HasOne(d => d.Coloration).WithMany(p => p.Detailcompositions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailco_detailcom_colorati");
        });

        modelBuilder.Entity<Detailpanier>(entity =>
        {
            entity.HasKey(e => new { e.Idproduit, e.Idcouleur, e.Idclient }).HasName("pk_detailpanier");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Detailpaniers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailpa_detailpan_client");

            entity.HasOne(d => d.Coloration).WithMany(p => p.Detailpaniers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detailpa_detailpan_colorati");
        });

        modelBuilder.Entity<Historiqueconsultation>(entity =>
        {
            entity.HasKey(e => new { e.Idclient, e.Idproduit }).HasName("pk_historiqueconsultation");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Historiqueconsultations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_historiq_historiqu_client");

            entity.HasOne(d => d.IdproduitNavigation).WithMany(p => p.Historiqueconsultations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_historiq_historiqu_produit");
        });

        modelBuilder.Entity<Messagechatbot>(entity =>
        {
            entity.HasKey(e => e.Idmessage).HasName("pk_messagechatbot");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Messagechatbots)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_messagec_clientcha_client");
        });

        modelBuilder.Entity<Paiement>(entity =>
        {
            entity.HasKey(e => e.Idpaiement).HasName("pk_paiement");

            entity.HasOne(d => d.IdcartebancaireNavigation).WithMany(p => p.Paiements)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_paiement_utilise_carteban");

            entity.HasOne(d => d.IdcommandeNavigation).WithMany(p => p.Paiements)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_paiement_paiementc_commande");

            entity.HasOne(d => d.IdtypepaiementNavigation).WithMany(p => p.Paiements)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_paiement_paiementt_typepaie");
        });

        modelBuilder.Entity<Pay>(entity =>
        {
            entity.HasKey(e => e.Idpays).HasName("pk_pays");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Idphoto).HasName("pk_photo");
        });

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.HasKey(e => e.Idproduit).HasName("pk_produit");

            entity.HasOne(d => d.IdpaysNavigation).WithMany(p => p.Produits)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_produit_paysorigi_pays");

            entity.HasOne(d => d.IdtypeproduitNavigation).WithMany(p => p.Produits)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_produit_produitty_typeprod");

            entity.HasMany(d => d.Idproduitsimilaire).WithMany(p => p.Idproduitsimilaire2)
                .UsingEntity<Dictionary<string, object>>(
                    "Produitsimilaire",
                    r => r.HasOne<Produit>().WithMany()
                        .HasForeignKey("Idproduit2")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_produits_produitsi_produit2"),
                    l => l.HasOne<Produit>().WithMany()
                        .HasForeignKey("Idproduit")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_produits_produitsi_produit"),
                    j =>
                    {
                        j.HasKey("Idproduit", "Idproduit2").HasName("pk_produitsimilaire");
                        j.ToTable("produitsimilaire");
                        j.HasIndex(new[] { "Idproduit2" }, "produitsimilaire2_fk");
                        j.HasIndex(new[] { "Idproduit" }, "produitsimilaire_fk");
                        j.HasIndex(new[] { "Idproduit", "Idproduit2" }, "produitsimilaire_pk").IsUnique();
                        j.IndexerProperty<int>("Idproduit").HasColumnName("idproduit");
                        j.IndexerProperty<int>("Idproduit2").HasColumnName("idproduit2");
                    });

            entity.HasMany(d => d.Idproduitsimilaire2).WithMany(p => p.Idproduitsimilaire)
                .UsingEntity<Dictionary<string, object>>(
                    "Produitsimilaire",
                    r => r.HasOne<Produit>().WithMany()
                        .HasForeignKey("Idproduit")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_produits_produitsi_produit"),
                    l => l.HasOne<Produit>().WithMany()
                        .HasForeignKey("Idproduit2")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("fk_produits_produitsi_produit2"),
                    j =>
                    {
                        j.HasKey("Idproduit", "Idproduit2").HasName("pk_produitsimilaire");
                        j.ToTable("produitsimilaire");
                        j.HasIndex(new[] { "Idproduit2" }, "produitsimilaire2_fk");
                        j.HasIndex(new[] { "Idproduit" }, "produitsimilaire_fk");
                        j.HasIndex(new[] { "Idproduit", "Idproduit2" }, "produitsimilaire_pk").IsUnique();
                        j.IndexerProperty<int>("Idproduit").HasColumnName("idproduit");
                        j.IndexerProperty<int>("Idproduit2").HasColumnName("idproduit2");
                    });
        });

        modelBuilder.Entity<Professionel>(entity =>
        {
            entity.HasKey(e => e.Idclient).HasName("pk_professionel");

            entity.Property(e => e.Idclient).ValueGeneratedNever();
            entity.Property(e => e.Nomsociete).ValueGeneratedOnAdd();
            entity.Property(e => e.Numtva).IsFixedLength();

            entity.HasOne(d => d.IdactiviteproNavigation).WithMany(p => p.Professionels)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_professi_proactivi_activite");

            entity.HasOne(d => d.IdclientNavigation).WithOne(p => p.Professionel)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_professi_heritagep_client");
        });

        modelBuilder.Entity<Regroupementproduit>(entity =>
        {
            entity.HasKey(e => e.Idregroupement).HasName("pk_regroupementproduit");
        });

        modelBuilder.Entity<Signalementavi>(entity =>
        {
            entity.HasKey(e => e.Idsignalement).HasName("pk_signalementavis");

            entity.HasOne(d => d.IdavisNavigation).WithMany(p => p.Signalementavis)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_signalem_avissigna_avisprod");

            entity.HasOne(d => d.IdtypesignalementNavigation).WithMany(p => p.Signalementavis)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_signalem_signaleme_typesign");
        });

        modelBuilder.Entity<Statutcommande>(entity =>
        {
            entity.HasKey(e => e.Idstatut).HasName("pk_statutcommande");
        });

        modelBuilder.Entity<Transporteur>(entity =>
        {
            entity.HasKey(e => e.Idtransporteur).HasName("pk_transporteur");
        });

        modelBuilder.Entity<Typepaiement>(entity =>
        {
            entity.HasKey(e => e.Idtypepaiement).HasName("pk_typepaiement");
        });

        modelBuilder.Entity<Typeproduit>(entity =>
        {
            entity.HasKey(e => e.Idtypeproduit).HasName("pk_typeproduit");

            entity.HasOne(d => d.IdcategorieNavigation).WithMany(p => p.Typeproduits)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_typeprod_categorie_categori");
        });

        modelBuilder.Entity<Typesignalement>(entity =>
        {
            entity.HasKey(e => e.Idtypesignalement).HasName("pk_typesignalement");
        });

        modelBuilder.Entity<Valeurattribut>(entity =>
        {
            entity.HasKey(e => new { e.Idattribut, e.Idproduit }).HasName("pk_valeurattribut");

            entity.HasOne(d => d.IdattributNavigation).WithMany(p => p.Valeurattributs)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_valeurat_valeuratt_attribut");

            entity.HasOne(d => d.IdproduitNavigation).WithMany(p => p.Valeurattributs)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_valeurat_valeuratt_produit");
        });

        modelBuilder.Entity<Ville>(entity =>
        {
            entity.HasKey(e => e.Codeinsee).HasName("pk_ville");

            entity.Property(e => e.Codeinsee).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
