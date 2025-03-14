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

    public virtual DbSet<Aime> Aimes { get; set; }

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

    public virtual DbSet<Detailregroupement> Detailregroupements { get; set; }

    public virtual DbSet<Detailpanier> Produitsimilaires { get; set; }

    public virtual DbSet<Historiqueconsultation> Historiqueconsultations { get; set; }

    public virtual DbSet<Messagechatbot> Messagechatbots { get; set; }

    public virtual DbSet<Paiement> Paiements { get; set; }

    public virtual DbSet<Pay> Pays { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Photoavi> Photoavis { get; set; }

    public virtual DbSet<Photocoloration> Photocolorations { get; set; }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activitepro>(entity =>
        {
            entity.HasKey(e => e.Idactivitepro).HasName("pk_act");
        });

        modelBuilder.Entity<Adresse>(entity =>
        {
            entity.HasKey(e => e.Idadresse).HasName("pk_adr");

            entity.Property(e => e.Codeinsee).IsFixedLength();
            entity.Property(e => e.Codepostaladresse).IsFixedLength();

            entity.HasOne(d => d.CodeinseeNavigation).WithMany(p => p.Adresses)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adr_vil");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Adresses)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adr_cli");

            entity.HasOne(d => d.IddepartementNavigation).WithMany(p => p.Adresses)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adr_dep");

            entity.HasOne(d => d.IdpaysNavigation).WithMany(p => p.Adresses)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adr_pay");
        });

        modelBuilder.Entity<Aime>(entity =>
        {
            entity.HasKey(e => new { e.Idclient, e.Idproduit }).HasName("pk_aim");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Aimes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_aim_cli");

            entity.HasOne(d => d.IdproduitNavigation).WithMany(p => p.Aimes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_aim_prd");
        });

        modelBuilder.Entity<Attributproduit>(entity =>
        {
            entity.HasKey(e => e.Idattribut).HasName("pk_att");

            entity.HasOne(d => d.IdtypeproduitNavigation).WithMany(p => p.Attributproduits)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_att_tpd");
        });

        modelBuilder.Entity<Avisproduit>(entity =>
        {
            entity.HasKey(e => e.Idavis).HasName("pk_avi");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Avisproduits)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_avi_cli");

            entity.HasOne(d => d.IdproduitNavigation).WithMany(p => p.Avisproduits)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_avi_prd");
        });

        modelBuilder.Entity<Cartebancaire>(entity =>
        {
            entity.HasKey(e => e.Idcartebancaire).HasName("pk_car");

            entity.Property(e => e.Numcartebancaire).IsFixedLength();

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Cartebancaires)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_car_cli");
        });

        modelBuilder.Entity<Categorieproduit>(entity =>
        {
            entity.HasKey(e => e.Idcategorie).HasName("pk_cat");

            entity.HasOne(d => d.CatIdcategorieNavigation).WithMany(p => p.InverseCatIdcategorieNavigation)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cat_cat");

            entity.HasOne(d => d.IdphotoNavigation).WithMany(p => p.Categorieproduits)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cat_pho");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Idclient).HasName("pk_cli");

            entity.Property(e => e.Datecreationcompte).IsFixedLength();
            entity.Property(e => e.Hashmdp).IsFixedLength();
            entity.Property(e => e.Telfixeclient).IsFixedLength();
            entity.Property(e => e.Telportableclient).IsFixedLength();
        });

        modelBuilder.Entity<Codepromo>(entity =>
        {
            entity.HasKey(e => e.Idcodepromo).HasName("pk_cod");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Codepromos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cod_cli");
        });

        modelBuilder.Entity<Coloration>(entity =>
        {
            entity.HasKey(e => new { e.Idproduit, e.Idcouleur }).HasName("pk_col");

            entity.HasOne(d => d.IdcouleurNavigation).WithMany(p => p.Colorations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_col_cou");

            entity.HasOne(d => d.IdproduitNavigation).WithMany(p => p.Colorations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_col_prd");
        });

        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasKey(e => e.Idcommande).HasName("pk_cmd");

            entity.HasOne(d => d.AdrIdadresseNavigation).WithMany(p => p.CommandeAdrIdadresseNavigations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cmd_adr");

            entity.HasOne(d => d.IdadresseNavigation).WithMany(p => p.CommandeIdadresseNavigations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cmd_adr2");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Commandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cmd_cli");

            entity.HasOne(d => d.IdcodepromoNavigation).WithMany(p => p.Commandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cmd_cod");

            entity.HasOne(d => d.IdstatutNavigation).WithMany(p => p.Commandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cmd_scd");

            entity.HasOne(d => d.IdtransporteurNavigation).WithMany(p => p.Commandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cmd_tpt");
        });

        modelBuilder.Entity<Commandecomposition>(entity =>
        {
            entity.HasKey(e => new { e.Idcomposition, e.Idcommande }).HasName("pk_cmc");

            entity.HasOne(d => d.IdcommandeNavigation).WithMany(p => p.Commandecompositions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cmc_cmd");

            entity.HasOne(d => d.IdcompositionNavigation).WithMany(p => p.Commandecompositions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cmd_cmp");
        });

        modelBuilder.Entity<Compositionproduit>(entity =>
        {
            entity.HasKey(e => e.Idcomposition).HasName("pk_cmp");
        });

        modelBuilder.Entity<Couleur>(entity =>
        {
            entity.HasKey(e => e.Idcouleur).HasName("pk_cou");

            entity.Property(e => e.Rgbcouleur).IsFixedLength();
        });

        modelBuilder.Entity<Departement>(entity =>
        {
            entity.HasKey(e => e.Iddepartement).HasName("pk_dep");
        });

        modelBuilder.Entity<Detailcommande>(entity =>
        {
            entity.HasKey(e => new { e.Idproduit, e.Idcouleur, e.Idcommande }).HasName("pk_dcm");

            entity.HasOne(d => d.IdcommandeNavigation).WithMany(p => p.Detailcommandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_dcm_cmd");

            entity.HasOne(d => d.Coloration).WithMany(p => p.Detailcommandes)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_dcm_col");
        });

        modelBuilder.Entity<Detailcomposition>(entity =>
        {
            entity.HasKey(e => new { e.Idproduit, e.Idcouleur, e.Idcomposition }).HasName("pk_dcp");

            entity.HasOne(d => d.IdcompositionNavigation).WithMany(p => p.Detailcompositions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_dcp_cmp");

            entity.HasOne(d => d.Coloration).WithMany(p => p.Detailcompositions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_dcp_col");
        });

        modelBuilder.Entity<Detailpanier>(entity =>
        {
            entity.HasKey(e => new { e.Idproduit, e.Idcouleur, e.Idclient }).HasName("pk_dpn");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Detailpaniers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_dpn_cli");

            entity.HasOne(d => d.Coloration).WithMany(p => p.Detailpaniers)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_dpn_col");
        });

        modelBuilder.Entity<Detailregroupement>(entity =>
        {
            entity.HasKey(e => new { e.Idproduit, e.Idcouleur, e.Idregroupement }).HasName("pk_drg");

            entity.HasOne(d => d.Colorations).WithMany(p => p.Detailregroupements)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_drg_col");

            entity.HasOne(d => d.IdregroupementNavigation).WithMany(p => p.Detailregroupements)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_drg_rgp");
        });

        modelBuilder.Entity<Produitsimilaire>(entity =>
        {
            entity.HasKey(e => new { e.Idproduit, e.Idproduit2 }).HasName("pk_pds");

            entity.HasOne(d => d.IdproduitNavigation).WithMany(p => p.Idproduitsimilaire)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_dps_prd");

            entity.HasOne(d => d.IdproduitNavigation2).WithMany(p => p.Idproduitsimilaire2)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_dps_prd2");
        });

        modelBuilder.Entity<Detailpaniercomposition>(entity =>
        {
            entity.HasKey(e => new { e.Idcomposition, e.Idclient }).HasName("pk_dpc");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Detailpaniercompositions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_dpc_cli");

            entity.HasOne(d => d.Composition).WithMany(p => p.Detailpaniercompositions)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_dpc_col");
        });

        modelBuilder.Entity<Historiqueconsultation>(entity =>
        {
            entity.HasKey(e => new { e.Idclient, e.Idproduit }).HasName("pk_hst");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Historiqueconsultations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_hst_cli");

            entity.HasOne(d => d.IdproduitNavigation).WithMany(p => p.Historiqueconsultations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_hst_prd");
        });

        modelBuilder.Entity<Messagechatbot>(entity =>
        {
            entity.HasKey(e => e.Idmessage).HasName("pk_msg");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.Messagechatbots)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_msg_cli");
        });

        modelBuilder.Entity<Paiement>(entity =>
        {
            entity.HasKey(e => e.Idpaiement).HasName("pk_pmt");

            entity.HasOne(d => d.IdcartebancaireNavigation).WithMany(p => p.Paiements)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_pmt_car");

            entity.HasOne(d => d.IdcommandeNavigation).WithMany(p => p.Paiements)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_pmt_cmd");

            entity.HasOne(d => d.IdtypepaiementNavigation).WithMany(p => p.Paiements)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_pmt_tpm");
        });

        modelBuilder.Entity<Pay>(entity =>
        {
            entity.HasKey(e => e.Idpays).HasName("pk_pay");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Idphoto).HasName("pk_pho");
        });

        modelBuilder.Entity<Photoavi>(entity =>
        {
            entity.HasKey(e => new { e.Idavis, e.Idphoto }).HasName("pk_pav");

            entity.HasOne(d => d.IdavisNavigation).WithMany(p => p.Photoavis)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_pav_avi");

            entity.HasOne(d => d.IdphotoNavigation).WithMany(p => p.Photoavis)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_pav_pho");
        });

        modelBuilder.Entity<Photocoloration>(entity =>
        {
            entity.HasKey(e => new { e.Idproduit, e.Idcouleur, e.Idphoto }).HasName("pk_pco");

            entity.HasOne(d => d.Colorations).WithMany(p => p.Photocolorations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_pco_col");

            entity.HasOne(d => d.IdphotoNavigation).WithMany(p => p.Photocolorations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_pco_pho");
        });

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.HasKey(e => e.Idproduit).HasName("pk_prd");

            entity.HasOne(d => d.IdpaysNavigation).WithMany(p => p.Produits)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_prd_pay");

            entity.HasOne(d => d.IdtypeproduitNavigation).WithMany(p => p.Produits)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_prd_tpd");
        });

        modelBuilder.Entity<Professionel>(entity =>
        {
            entity.HasKey(e => e.Idclient).HasName("pk_pro");

            entity.Property(e => e.Idclient).ValueGeneratedNever();
            entity.Property(e => e.Nomsociete).ValueGeneratedOnAdd();
            entity.Property(e => e.Numtva).IsFixedLength();

            entity.HasOne(d => d.IdactiviteproNavigation).WithMany(p => p.Professionels)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_pro_act");

            entity.HasOne(d => d.IdclientNavigation).WithOne(p => p.Professionel)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_pro_cli");
        });

        modelBuilder.Entity<Regroupementproduit>(entity =>
        {
            entity.HasKey(e => e.Idregroupement).HasName("pk_rgp");
        });

        modelBuilder.Entity<Signalementavi>(entity =>
        {
            entity.HasKey(e => e.Idsignalement).HasName("pk_sga");

            entity.HasOne(d => d.IdavisNavigation).WithMany(p => p.Signalementavis)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_sga_avi");

            entity.HasOne(d => d.IdtypesignalementNavigation).WithMany(p => p.Signalementavis)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_sga_tsg");
        });

        modelBuilder.Entity<Statutcommande>(entity =>
        {
            entity.HasKey(e => e.Idstatut).HasName("pk_scd");
        });

        modelBuilder.Entity<Transporteur>(entity =>
        {
            entity.HasKey(e => e.Idtransporteur).HasName("pk_tpt");
        });

        modelBuilder.Entity<Typepaiement>(entity =>
        {
            entity.HasKey(e => e.Idtypepaiement).HasName("pk_tpm");
        });

        modelBuilder.Entity<Typeproduit>(entity =>
        {
            entity.HasKey(e => e.Idtypeproduit).HasName("pk_tpd");

            entity.HasOne(d => d.IdcategorieNavigation).WithMany(p => p.Typeproduits)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_tpd_cat");
        });

        modelBuilder.Entity<Typesignalement>(entity =>
        {
            entity.HasKey(e => e.Idtypesignalement).HasName("pk_tsg");
        });

        modelBuilder.Entity<Valeurattribut>(entity =>
        {
            entity.HasKey(e => new { e.Idattribut, e.Idproduit }).HasName("pk_val");

            entity.HasOne(d => d.IdattributNavigation).WithMany(p => p.Valeurattributs)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_val_att");

            entity.HasOne(d => d.IdproduitNavigation).WithMany(p => p.Valeurattributs)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_val_prd");
        });

        modelBuilder.Entity<Ville>(entity =>
        {
            entity.HasKey(e => e.Codeinsee).HasName("pk_vil");

            entity.Property(e => e.Codeinsee).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
