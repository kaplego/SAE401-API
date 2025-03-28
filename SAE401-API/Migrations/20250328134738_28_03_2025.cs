using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SAE401_API.Migrations
{
    /// <inheritdoc />
    public partial class _28_03_2025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "seq_adr");

            migrationBuilder.CreateSequence<int>(
                name: "seq_avi");

            migrationBuilder.CreateSequence<int>(
                name: "seq_car");

            migrationBuilder.CreateSequence<int>(
                name: "seq_cli");

            migrationBuilder.CreateSequence<int>(
                name: "seq_cmd");

            migrationBuilder.CreateSequence<int>(
                name: "seq_msg");

            migrationBuilder.CreateSequence<int>(
                name: "seq_pho");

            migrationBuilder.CreateSequence<int>(
                name: "seq_pmt");

            migrationBuilder.CreateSequence<int>(
                name: "seq_prd");

            migrationBuilder.CreateSequence<int>(
                name: "seq_sga");

            migrationBuilder.CreateTable(
                name: "t_e_activitepro_act",
                columns: table => new
                {
                    act_idactivitepro = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    act_nomactivitepro = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_act", x => x.act_idactivitepro);
                });

            migrationBuilder.CreateTable(
                name: "t_e_client_cli",
                columns: table => new
                {
                    cli_idclient = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('seq_cli')"),
                    cli_nomclient = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    cli_prenomclient = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    cli_civiliteclient = table.Column<char>(type: "character(1)", nullable: true),
                    cli_emailclient = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    cli_telfixeclient = table.Column<string>(type: "character(11)", fixedLength: true, maxLength: 11, nullable: true),
                    cli_telportableclient = table.Column<string>(type: "character(11)", fixedLength: true, maxLength: 11, nullable: false),
                    cli_datecreationcompte = table.Column<DateTime>(type: "timestamp with time zone", fixedLength: true, nullable: false, defaultValueSql: "now()"),
                    cli_hashmdp = table.Column<string>(type: "character(256)", fixedLength: true, maxLength: 256, nullable: false),
                    cli_pointfideliteclient = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    cli_newslettermiliboo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    cli_newsletterpartenaires = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cli", x => x.cli_idclient);
                });

            migrationBuilder.CreateTable(
                name: "t_e_compositionproduit_cmp",
                columns: table => new
                {
                    cmp_idcomposition = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cmp_nomcomposition = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    cmp_prixventecomposition = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    cmp_prixsoldecomposition = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    cmp_descriptioncomposition = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cmp", x => x.cmp_idcomposition);
                });

            migrationBuilder.CreateTable(
                name: "t_e_couleur_cou",
                columns: table => new
                {
                    cou_idcouleur = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cou_nomcouleur = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    cou_rgbcouleur = table.Column<string>(type: "character(6)", fixedLength: true, maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cou", x => x.cou_idcouleur);
                });

            migrationBuilder.CreateTable(
                name: "t_e_departement_dep",
                columns: table => new
                {
                    dep_iddepartement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dep_nomdepartement = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dep", x => x.dep_iddepartement);
                });

            migrationBuilder.CreateTable(
                name: "t_e_pays_pay",
                columns: table => new
                {
                    pay_idpays = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pay_nompays = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pay", x => x.pay_idpays);
                });

            migrationBuilder.CreateTable(
                name: "t_e_photo_pho",
                columns: table => new
                {
                    pho_idphoto = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('seq_pho')"),
                    pho_sourcephoto = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    pho_descriptionphoto = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pho", x => x.pho_idphoto);
                });

            migrationBuilder.CreateTable(
                name: "t_e_regroupementproduit_rgp",
                columns: table => new
                {
                    rgp_idregroupement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rgp_nomregroupement = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rgp", x => x.rgp_idregroupement);
                });

            migrationBuilder.CreateTable(
                name: "t_e_statutcommande_scd",
                columns: table => new
                {
                    scd_idstatut = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scd_nomstatut = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_scd", x => x.scd_idstatut);
                });

            migrationBuilder.CreateTable(
                name: "t_e_transporteur_tpt",
                columns: table => new
                {
                    tpt_idtransporteur = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tpt_nomtransporteur = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tpt", x => x.tpt_idtransporteur);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typepaiement_tpm",
                columns: table => new
                {
                    tpm_idtypepaiement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tpm_nomtypepaiement = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tpm", x => x.tpm_idtypepaiement);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typesignalement_tsg",
                columns: table => new
                {
                    tsg_idtypesignalement = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tsg_nomtypesignalement = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tsg", x => x.tsg_idtypesignalement);
                });

            migrationBuilder.CreateTable(
                name: "t_e_ville_vil",
                columns: table => new
                {
                    vil_codeinsee = table.Column<string>(type: "character(5)", fixedLength: true, maxLength: 5, nullable: false),
                    vil_nomville = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vil", x => x.vil_codeinsee);
                });

            migrationBuilder.CreateTable(
                name: "t_e_cartebancaire_car",
                columns: table => new
                {
                    car_idcartebancaire = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('seq_car')"),
                    car_idclient = table.Column<int>(type: "integer", nullable: false),
                    car_titulairecartebancaire = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    car_nomcartebancaire = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    car_dateenregistrement = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    car_numcartebancaire = table.Column<string>(type: "character(16)", fixedLength: true, maxLength: 16, nullable: false),
                    car_dateexpirationcarte = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_car", x => x.car_idcartebancaire);
                    table.ForeignKey(
                        name: "fk_car_cli",
                        column: x => x.car_idclient,
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_idclient",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_codepromo_cod",
                columns: table => new
                {
                    cod_idcodepromo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cod_idclient = table.Column<int>(type: "integer", nullable: true),
                    cod_nomcodepromo = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    cod_valeurreduction = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    cod_estvalide = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    cod_dateexpirationcode = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cod", x => x.cod_idcodepromo);
                    table.ForeignKey(
                        name: "fk_cod_cli",
                        column: x => x.cod_idclient,
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_idclient",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_messagechatbot_msg",
                columns: table => new
                {
                    msg_idmessage = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('seq_msg')"),
                    msg_idclient = table.Column<int>(type: "integer", nullable: false),
                    msg_contenumessage = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    msg_reponsemessage = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_msg", x => x.msg_idmessage);
                    table.ForeignKey(
                        name: "fk_msg_cli",
                        column: x => x.msg_idclient,
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_idclient",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_h_professionel_pro",
                columns: table => new
                {
                    pro_idclient = table.Column<int>(type: "integer", nullable: false),
                    pro_idactivitepro = table.Column<int>(type: "integer", nullable: false),
                    pro_nomsociete = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    pro_numtva = table.Column<string>(type: "character(11)", fixedLength: true, maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pro", x => x.pro_idclient);
                    table.ForeignKey(
                        name: "fk_pro_act",
                        column: x => x.pro_idactivitepro,
                        principalTable: "t_e_activitepro_act",
                        principalColumn: "act_idactivitepro",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pro_cli",
                        column: x => x.pro_idclient,
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_idclient",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_detailpaniercomposition_dpc",
                columns: table => new
                {
                    dpc_idcomposition = table.Column<int>(type: "integer", nullable: false),
                    dpc_idclient = table.Column<int>(type: "integer", nullable: false),
                    dpc_quantitepaniercomposition = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dpc", x => new { x.dpc_idcomposition, x.dpc_idclient });
                    table.ForeignKey(
                        name: "fk_dpc_cli",
                        column: x => x.dpc_idclient,
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_idclient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_dpc_col",
                        column: x => x.dpc_idcomposition,
                        principalTable: "t_e_compositionproduit_cmp",
                        principalColumn: "cmp_idcomposition",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_categorieproduit_cat",
                columns: table => new
                {
                    cat_idcategorie = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cat_idcategorie2 = table.Column<int>(type: "integer", nullable: true),
                    cat_idphoto = table.Column<int>(type: "integer", nullable: true),
                    cat_nomcategorie = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    cat_descriptioncategorie = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    cat_estfiltrable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cat", x => x.cat_idcategorie);
                    table.ForeignKey(
                        name: "fk_cat_cat",
                        column: x => x.cat_idcategorie2,
                        principalTable: "t_e_categorieproduit_cat",
                        principalColumn: "cat_idcategorie",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cat_pho",
                        column: x => x.cat_idphoto,
                        principalTable: "t_e_photo_pho",
                        principalColumn: "pho_idphoto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_adresse_adr",
                columns: table => new
                {
                    adr_idadresse = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('seq_adr')"),
                    adr_idpays = table.Column<int>(type: "integer", nullable: false),
                    adr_codeinsee = table.Column<string>(type: "character(5)", fixedLength: true, maxLength: 5, nullable: false),
                    adr_idclient = table.Column<int>(type: "integer", nullable: false),
                    adr_iddepartement = table.Column<int>(type: "integer", nullable: false),
                    adr_nomadresse = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    adr_numerorue = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    adr_nomrue = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    adr_codepostaladresse = table.Column<string>(type: "character(5)", fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_adr", x => x.adr_idadresse);
                    table.ForeignKey(
                        name: "fk_adr_cli",
                        column: x => x.adr_idclient,
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_idclient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_adr_dep",
                        column: x => x.adr_iddepartement,
                        principalTable: "t_e_departement_dep",
                        principalColumn: "dep_iddepartement",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_adr_pay",
                        column: x => x.adr_idpays,
                        principalTable: "t_e_pays_pay",
                        principalColumn: "pay_idpays",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_adr_vil",
                        column: x => x.adr_codeinsee,
                        principalTable: "t_e_ville_vil",
                        principalColumn: "vil_codeinsee",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeproduit_tpd",
                columns: table => new
                {
                    tpd_idtypeproduit = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tpd_idcategorie = table.Column<int>(type: "integer", nullable: false),
                    tpd_nomtypeproduit = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tpd", x => x.tpd_idtypeproduit);
                    table.ForeignKey(
                        name: "fk_tpd_cat",
                        column: x => x.tpd_idcategorie,
                        principalTable: "t_e_categorieproduit_cat",
                        principalColumn: "cat_idcategorie",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_commande_cmd",
                columns: table => new
                {
                    cmd_idcommande = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('seq_cmd')"),
                    cmd_idclient = table.Column<int>(type: "integer", nullable: false),
                    cmd_idadresse = table.Column<int>(type: "integer", nullable: false),
                    cmd_idcodepromo = table.Column<int>(type: "integer", nullable: true),
                    cmd_adr_idadresse = table.Column<int>(type: "integer", nullable: false),
                    cmd_idstatut = table.Column<int>(type: "integer", nullable: false),
                    cmd_idtransporteur = table.Column<int>(type: "integer", nullable: false),
                    cmd_datecommande = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    cmd_avecassurance = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    cmd_aveclivraisonexpress = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    cmd_instructionlivraison = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cmd", x => x.cmd_idcommande);
                    table.ForeignKey(
                        name: "fk_cmd_adr",
                        column: x => x.cmd_adr_idadresse,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_idadresse",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cmd_adr2",
                        column: x => x.cmd_idadresse,
                        principalTable: "t_e_adresse_adr",
                        principalColumn: "adr_idadresse",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cmd_cli",
                        column: x => x.cmd_idclient,
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_idclient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cmd_cod",
                        column: x => x.cmd_idcodepromo,
                        principalTable: "t_e_codepromo_cod",
                        principalColumn: "cod_idcodepromo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cmd_scd",
                        column: x => x.cmd_idstatut,
                        principalTable: "t_e_statutcommande_scd",
                        principalColumn: "scd_idstatut",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cmd_tpt",
                        column: x => x.cmd_idtransporteur,
                        principalTable: "t_e_transporteur_tpt",
                        principalColumn: "tpt_idtransporteur",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_attributproduit_att",
                columns: table => new
                {
                    att_idattribut = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    att_idtypeproduit = table.Column<int>(type: "integer", nullable: false),
                    att_nomattribut = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_att", x => x.att_idattribut);
                    table.ForeignKey(
                        name: "fk_att_tpd",
                        column: x => x.att_idtypeproduit,
                        principalTable: "t_e_typeproduit_tpd",
                        principalColumn: "tpd_idtypeproduit",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_produit_prd",
                columns: table => new
                {
                    prd_idproduit = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('seq_prd')"),
                    prd_idtypeproduit = table.Column<int>(type: "integer", nullable: false),
                    prd_idpays = table.Column<int>(type: "integer", nullable: false),
                    prd_nomproduit = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    prd_notice = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    prd_aspecttechnique = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    prd_delailivraison = table.Column<int>(type: "integer", nullable: false, defaultValue: 72),
                    prd_coutlivraison = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    prd_nbpaiementmax = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prd", x => x.prd_idproduit);
                    table.ForeignKey(
                        name: "fk_prd_pay",
                        column: x => x.prd_idpays,
                        principalTable: "t_e_pays_pay",
                        principalColumn: "pay_idpays",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_prd_tpd",
                        column: x => x.prd_idtypeproduit,
                        principalTable: "t_e_typeproduit_tpd",
                        principalColumn: "tpd_idtypeproduit",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_paiement_pmt",
                columns: table => new
                {
                    pmt_idpaiement = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('seq_pmt')"),
                    pmt_idcartebancaire = table.Column<int>(type: "integer", nullable: true),
                    pmt_idcommande = table.Column<int>(type: "integer", nullable: false),
                    pmt_idtypepaiement = table.Column<int>(type: "integer", nullable: false),
                    pmt_datepaiement = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    pmt_montantpaiement = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    pmt_indicepaiement = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pmt", x => x.pmt_idpaiement);
                    table.ForeignKey(
                        name: "fk_pmt_car",
                        column: x => x.pmt_idcartebancaire,
                        principalTable: "t_e_cartebancaire_car",
                        principalColumn: "car_idcartebancaire",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pmt_cmd",
                        column: x => x.pmt_idcommande,
                        principalTable: "t_e_commande_cmd",
                        principalColumn: "cmd_idcommande",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pmt_tpm",
                        column: x => x.pmt_idtypepaiement,
                        principalTable: "t_e_typepaiement_tpm",
                        principalColumn: "tpm_idtypepaiement",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_commandecomposition_cmc",
                columns: table => new
                {
                    cmc_idcomposition = table.Column<int>(type: "integer", nullable: false),
                    cmc_idcommande = table.Column<int>(type: "integer", nullable: false),
                    cmc_quantitecompositioncommande = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cmc", x => new { x.cmc_idcomposition, x.cmc_idcommande });
                    table.ForeignKey(
                        name: "fk_cmc_cmd",
                        column: x => x.cmc_idcommande,
                        principalTable: "t_e_commande_cmd",
                        principalColumn: "cmd_idcommande",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cmd_cmp",
                        column: x => x.cmc_idcomposition,
                        principalTable: "t_e_compositionproduit_cmp",
                        principalColumn: "cmp_idcomposition",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_avisproduit_avi",
                columns: table => new
                {
                    avi_idavis = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('seq_avi')"),
                    avi_idproduit = table.Column<int>(type: "integer", nullable: false),
                    avi_idclient = table.Column<int>(type: "integer", nullable: false),
                    avi_noteavis = table.Column<int>(type: "integer", nullable: false),
                    avi_dateavis = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    avi_commentaireavis = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    avi_reponsemiliboo = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_avi", x => x.avi_idavis);
                    table.ForeignKey(
                        name: "fk_avi_cli",
                        column: x => x.avi_idclient,
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_idclient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_avi_prd",
                        column: x => x.avi_idproduit,
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_idproduit",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_aime_aim",
                columns: table => new
                {
                    aim_idclient = table.Column<int>(type: "integer", nullable: false),
                    aim_idproduit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_aim", x => new { x.aim_idclient, x.aim_idproduit });
                    table.ForeignKey(
                        name: "fk_aim_cli",
                        column: x => x.aim_idclient,
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_idclient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_aim_prd",
                        column: x => x.aim_idproduit,
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_idproduit",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_coloration_col",
                columns: table => new
                {
                    col_idproduit = table.Column<int>(type: "integer", nullable: false),
                    col_idcouleur = table.Column<int>(type: "integer", nullable: false),
                    col_prixvente = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    col_prixsolde = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    col_quantitestock = table.Column<int>(type: "integer", nullable: false),
                    col_descriptioncoloration = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    col_estvisible = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_col", x => new { x.col_idproduit, x.col_idcouleur });
                    table.ForeignKey(
                        name: "fk_col_cou",
                        column: x => x.col_idcouleur,
                        principalTable: "t_e_couleur_cou",
                        principalColumn: "cou_idcouleur",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_col_prd",
                        column: x => x.col_idproduit,
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_idproduit",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_historiqueconsultation_hst",
                columns: table => new
                {
                    hst_idclient = table.Column<int>(type: "integer", nullable: false),
                    hst_idproduit = table.Column<int>(type: "integer", nullable: false),
                    hst_dateconsultation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hst", x => new { x.hst_idclient, x.hst_idproduit });
                    table.ForeignKey(
                        name: "fk_hst_cli",
                        column: x => x.hst_idclient,
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_idclient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_hst_prd",
                        column: x => x.hst_idproduit,
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_idproduit",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_produitsimilaire_pds",
                columns: table => new
                {
                    pds_idproduit = table.Column<int>(type: "integer", nullable: false),
                    pds_idproduit2 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pds", x => new { x.pds_idproduit2, x.pds_idproduit });
                    table.ForeignKey(
                        name: "fk_dps_prd",
                        column: x => x.pds_idproduit,
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_idproduit",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_dps_prd2",
                        column: x => x.pds_idproduit2,
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_idproduit",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_valeurattribut_val",
                columns: table => new
                {
                    val_idattribut = table.Column<int>(type: "integer", nullable: false),
                    val_idproduit = table.Column<int>(type: "integer", nullable: false),
                    val_valeur = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_val", x => new { x.val_idattribut, x.val_idproduit });
                    table.ForeignKey(
                        name: "fk_val_att",
                        column: x => x.val_idattribut,
                        principalTable: "t_e_attributproduit_att",
                        principalColumn: "att_idattribut",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_val_prd",
                        column: x => x.val_idproduit,
                        principalTable: "t_e_produit_prd",
                        principalColumn: "prd_idproduit",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_signalementavis_sga",
                columns: table => new
                {
                    sga_idsignalement = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('seq_sga')"),
                    sga_idavis = table.Column<int>(type: "integer", nullable: false),
                    sga_idtypesignalement = table.Column<int>(type: "integer", nullable: false),
                    sga_emailsignalement = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    sga_datesignalement = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    sga_contenusignalement = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sga", x => x.sga_idsignalement);
                    table.ForeignKey(
                        name: "fk_sga_avi",
                        column: x => x.sga_idavis,
                        principalTable: "t_e_avisproduit_avi",
                        principalColumn: "avi_idavis",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sga_tsg",
                        column: x => x.sga_idtypesignalement,
                        principalTable: "t_e_typesignalement_tsg",
                        principalColumn: "tsg_idtypesignalement",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_photoavis_pav",
                columns: table => new
                {
                    pav_idavis = table.Column<int>(type: "integer", nullable: false),
                    pav_idproduit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pav", x => new { x.pav_idavis, x.pav_idproduit });
                    table.ForeignKey(
                        name: "fk_pav_avi",
                        column: x => x.pav_idavis,
                        principalTable: "t_e_avisproduit_avi",
                        principalColumn: "avi_idavis",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pav_pho",
                        column: x => x.pav_idproduit,
                        principalTable: "t_e_photo_pho",
                        principalColumn: "pho_idphoto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_detailcommande_dcm",
                columns: table => new
                {
                    dcm_idproduit = table.Column<int>(type: "integer", nullable: false),
                    dcm_idcouleur = table.Column<int>(type: "integer", nullable: false),
                    dcm_idcommande = table.Column<int>(type: "integer", nullable: false),
                    dcm_quantitecommande = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dcm", x => new { x.dcm_idproduit, x.dcm_idcouleur, x.dcm_idcommande });
                    table.ForeignKey(
                        name: "fk_dcm_cmd",
                        column: x => x.dcm_idcommande,
                        principalTable: "t_e_commande_cmd",
                        principalColumn: "cmd_idcommande",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_dcm_col",
                        columns: x => new { x.dcm_idproduit, x.dcm_idcouleur },
                        principalTable: "t_j_coloration_col",
                        principalColumns: new[] { "col_idproduit", "col_idcouleur" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_detailcomposition_dcp",
                columns: table => new
                {
                    dcp_idproduit = table.Column<int>(type: "integer", nullable: false),
                    dcp_idcouleur = table.Column<int>(type: "integer", nullable: false),
                    dcp_idcomposition = table.Column<int>(type: "integer", nullable: false),
                    dcp_quantitecomposition = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dcp", x => new { x.dcp_idproduit, x.dcp_idcouleur, x.dcp_idcomposition });
                    table.ForeignKey(
                        name: "fk_dcp_cmp",
                        column: x => x.dcp_idcomposition,
                        principalTable: "t_e_compositionproduit_cmp",
                        principalColumn: "cmp_idcomposition",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_dcp_col",
                        columns: x => new { x.dcp_idproduit, x.dcp_idcouleur },
                        principalTable: "t_j_coloration_col",
                        principalColumns: new[] { "col_idproduit", "col_idcouleur" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_detailpanier_dpn",
                columns: table => new
                {
                    dpn_idproduit = table.Column<int>(type: "integer", nullable: false),
                    dpn_idcouleur = table.Column<int>(type: "integer", nullable: false),
                    dpn_idclient = table.Column<int>(type: "integer", nullable: false),
                    dpn_quantitepanier = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dpn", x => new { x.dpn_idproduit, x.dpn_idcouleur, x.dpn_idclient });
                    table.ForeignKey(
                        name: "fk_dpn_cli",
                        column: x => x.dpn_idclient,
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_idclient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_dpn_col",
                        columns: x => new { x.dpn_idproduit, x.dpn_idcouleur },
                        principalTable: "t_j_coloration_col",
                        principalColumns: new[] { "col_idproduit", "col_idcouleur" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_detailregroupement_drg",
                columns: table => new
                {
                    drg_idproduit = table.Column<int>(type: "integer", nullable: false),
                    drg_idcouleur = table.Column<int>(type: "integer", nullable: false),
                    drg_idregroupement = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_drg", x => new { x.drg_idproduit, x.drg_idcouleur, x.drg_idregroupement });
                    table.ForeignKey(
                        name: "fk_drg_col",
                        columns: x => new { x.drg_idproduit, x.drg_idcouleur },
                        principalTable: "t_j_coloration_col",
                        principalColumns: new[] { "col_idproduit", "col_idcouleur" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_drg_rgp",
                        column: x => x.drg_idregroupement,
                        principalTable: "t_e_regroupementproduit_rgp",
                        principalColumn: "rgp_idregroupement",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_j_photocoloration_pco",
                columns: table => new
                {
                    pco_idproduit = table.Column<int>(type: "integer", nullable: false),
                    pco_idcouleur = table.Column<int>(type: "integer", nullable: false),
                    pco_idphoto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pco", x => new { x.pco_idproduit, x.pco_idcouleur, x.pco_idphoto });
                    table.ForeignKey(
                        name: "fk_pco_col",
                        columns: x => new { x.pco_idproduit, x.pco_idcouleur },
                        principalTable: "t_j_coloration_col",
                        principalColumns: new[] { "col_idproduit", "col_idcouleur" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pco_pho",
                        column: x => x.pco_idphoto,
                        principalTable: "t_e_photo_pho",
                        principalColumn: "pho_idphoto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_t_e_activitepro_act_nomactivitepro",
                table: "t_e_activitepro_act",
                column: "act_nomactivitepro",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_adr_codeinsee",
                table: "t_e_adresse_adr",
                column: "adr_codeinsee");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_adr_idclient",
                table: "t_e_adresse_adr",
                column: "adr_idclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_adr_iddepartement",
                table: "t_e_adresse_adr",
                column: "adr_iddepartement");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_adr_idpays",
                table: "t_e_adresse_adr",
                column: "adr_idpays");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_attributproduit_att_idtypeproduit_nomattribut",
                table: "t_e_attributproduit_att",
                columns: new[] { "att_idtypeproduit", "att_nomattribut" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_avisproduit_avi_avi_idproduit",
                table: "t_e_avisproduit_avi",
                column: "avi_idproduit");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_avisproduit_avi_idclient_idproduit",
                table: "t_e_avisproduit_avi",
                columns: new[] { "avi_idclient", "avi_idproduit" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_t_e_cartebancaire_car_idclient_nomcartebancaire",
                table: "t_e_cartebancaire_car",
                columns: new[] { "car_idclient", "car_nomcartebancaire" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_t_e_cartebancaire_car_idclient_numcartebancaire",
                table: "t_e_cartebancaire_car",
                columns: new[] { "car_idclient", "car_numcartebancaire" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_categorieproduit_cat_cat_idcategorie2",
                table: "t_e_categorieproduit_cat",
                column: "cat_idcategorie2");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_categorieproduit_cat_cat_idphoto",
                table: "t_e_categorieproduit_cat",
                column: "cat_idphoto");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_categorieproduit_cat_nomcategorie",
                table: "t_e_categorieproduit_cat",
                column: "cat_nomcategorie",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_t_e_client_cli_nomclient_prenomclient_telportableclient",
                table: "t_e_client_cli",
                columns: new[] { "cli_nomclient", "cli_prenomclient", "cli_telportableclient" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_codepromo_cod_cod_idclient",
                table: "t_e_codepromo_cod",
                column: "cod_idclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_cmd_adr_idadresse",
                table: "t_e_commande_cmd",
                column: "cmd_adr_idadresse");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_cmd_idadresse",
                table: "t_e_commande_cmd",
                column: "cmd_idadresse");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_cmd_idclient",
                table: "t_e_commande_cmd",
                column: "cmd_idclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_cmd_idcodepromo",
                table: "t_e_commande_cmd",
                column: "cmd_idcodepromo");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_cmd_idstatut",
                table: "t_e_commande_cmd",
                column: "cmd_idstatut");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commande_cmd_cmd_idtransporteur",
                table: "t_e_commande_cmd",
                column: "cmd_idtransporteur");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_compositionproduit_cmp_nomcomposition",
                table: "t_e_compositionproduit_cmp",
                column: "cmp_nomcomposition",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_t_e_couleur_cou_nomcouleur",
                table: "t_e_couleur_cou",
                column: "cou_nomcouleur",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_messagechatbot_msg_msg_idclient",
                table: "t_e_messagechatbot_msg",
                column: "msg_idclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_paiement_pmt_pmt_idcartebancaire",
                table: "t_e_paiement_pmt",
                column: "pmt_idcartebancaire");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_paiement_pmt_pmt_idcommande",
                table: "t_e_paiement_pmt",
                column: "pmt_idcommande");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_paiement_pmt_pmt_idtypepaiement",
                table: "t_e_paiement_pmt",
                column: "pmt_idtypepaiement");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_pays_pay_nompays",
                table: "t_e_pays_pay",
                column: "pay_nompays",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_t_e_produit_prd_nomproduit",
                table: "t_e_produit_prd",
                column: "prd_nomproduit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_prd_prd_idpays",
                table: "t_e_produit_prd",
                column: "prd_idpays");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_prd_prd_idtypeproduit",
                table: "t_e_produit_prd",
                column: "prd_idtypeproduit");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_regroupementproduit_rgp_nomregroupement",
                table: "t_e_regroupementproduit_rgp",
                column: "rgp_nomregroupement",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_t_e_signalementavis_sga_idavis_emailsignalement",
                table: "t_e_signalementavis_sga",
                columns: new[] { "sga_idavis", "sga_emailsignalement" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_signalementavis_sga_sga_idtypesignalement",
                table: "t_e_signalementavis_sga",
                column: "sga_idtypesignalement");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_statutcommande_scd_nomstatut",
                table: "t_e_statutcommande_scd",
                column: "scd_nomstatut",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_t_e_transporteur_tpt_nomtransporteur",
                table: "t_e_transporteur_tpt",
                column: "tpt_nomtransporteur",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_t_e_typepaiement_tpm_nomtypepaiement",
                table: "t_e_typepaiement_tpm",
                column: "tpm_nomtypepaiement",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_t_e_typeproduit_tpd_nomtypeproduit",
                table: "t_e_typeproduit_tpd",
                column: "tpd_nomtypeproduit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_typeproduit_tpd_tpd_idcategorie",
                table: "t_e_typeproduit_tpd",
                column: "tpd_idcategorie");

            migrationBuilder.CreateIndex(
                name: "ix_t_e_typesignalement_tsg_nomtypesignalement",
                table: "t_e_typesignalement_tsg",
                column: "tsg_nomtypesignalement",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_h_professionel_pro_pro_idactivitepro",
                table: "t_h_professionel_pro",
                column: "pro_idactivitepro");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_aime_aim_aim_idproduit",
                table: "t_j_aime_aim",
                column: "aim_idproduit");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_coloration_col_col_idcouleur",
                table: "t_j_coloration_col",
                column: "col_idcouleur");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_commandecomposition_cmc_cmc_idcommande",
                table: "t_j_commandecomposition_cmc",
                column: "cmc_idcommande");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_detailcommande_dcm_dcm_idcommande",
                table: "t_j_detailcommande_dcm",
                column: "dcm_idcommande");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_detailcomposition_dcp_dcp_idcomposition",
                table: "t_j_detailcomposition_dcp",
                column: "dcp_idcomposition");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_detailpanier_dpn_dpn_idclient",
                table: "t_j_detailpanier_dpn",
                column: "dpn_idclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_detailpaniercomposition_dpc_dpc_idclient",
                table: "t_j_detailpaniercomposition_dpc",
                column: "dpc_idclient");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_detailregroupement_drg_drg_idregroupement",
                table: "t_j_detailregroupement_drg",
                column: "drg_idregroupement");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_historiqueconsultation_hst_hst_idproduit",
                table: "t_j_historiqueconsultation_hst",
                column: "hst_idproduit");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_photoavis_pav_pav_idproduit",
                table: "t_j_photoavis_pav",
                column: "pav_idproduit");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_photocoloration_pco_pco_idphoto",
                table: "t_j_photocoloration_pco",
                column: "pco_idphoto");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_produitsimilaire_pds_pds_idproduit",
                table: "t_j_produitsimilaire_pds",
                column: "pds_idproduit");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_valeurattribut_val_val_idproduit",
                table: "t_j_valeurattribut_val",
                column: "val_idproduit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_messagechatbot_msg");

            migrationBuilder.DropTable(
                name: "t_e_paiement_pmt");

            migrationBuilder.DropTable(
                name: "t_e_signalementavis_sga");

            migrationBuilder.DropTable(
                name: "t_h_professionel_pro");

            migrationBuilder.DropTable(
                name: "t_j_aime_aim");

            migrationBuilder.DropTable(
                name: "t_j_commandecomposition_cmc");

            migrationBuilder.DropTable(
                name: "t_j_detailcommande_dcm");

            migrationBuilder.DropTable(
                name: "t_j_detailcomposition_dcp");

            migrationBuilder.DropTable(
                name: "t_j_detailpanier_dpn");

            migrationBuilder.DropTable(
                name: "t_j_detailpaniercomposition_dpc");

            migrationBuilder.DropTable(
                name: "t_j_detailregroupement_drg");

            migrationBuilder.DropTable(
                name: "t_j_historiqueconsultation_hst");

            migrationBuilder.DropTable(
                name: "t_j_photoavis_pav");

            migrationBuilder.DropTable(
                name: "t_j_photocoloration_pco");

            migrationBuilder.DropTable(
                name: "t_j_produitsimilaire_pds");

            migrationBuilder.DropTable(
                name: "t_j_valeurattribut_val");

            migrationBuilder.DropTable(
                name: "t_e_cartebancaire_car");

            migrationBuilder.DropTable(
                name: "t_e_typepaiement_tpm");

            migrationBuilder.DropTable(
                name: "t_e_typesignalement_tsg");

            migrationBuilder.DropTable(
                name: "t_e_activitepro_act");

            migrationBuilder.DropTable(
                name: "t_e_commande_cmd");

            migrationBuilder.DropTable(
                name: "t_e_compositionproduit_cmp");

            migrationBuilder.DropTable(
                name: "t_e_regroupementproduit_rgp");

            migrationBuilder.DropTable(
                name: "t_e_avisproduit_avi");

            migrationBuilder.DropTable(
                name: "t_j_coloration_col");

            migrationBuilder.DropTable(
                name: "t_e_attributproduit_att");

            migrationBuilder.DropTable(
                name: "t_e_adresse_adr");

            migrationBuilder.DropTable(
                name: "t_e_codepromo_cod");

            migrationBuilder.DropTable(
                name: "t_e_statutcommande_scd");

            migrationBuilder.DropTable(
                name: "t_e_transporteur_tpt");

            migrationBuilder.DropTable(
                name: "t_e_couleur_cou");

            migrationBuilder.DropTable(
                name: "t_e_produit_prd");

            migrationBuilder.DropTable(
                name: "t_e_departement_dep");

            migrationBuilder.DropTable(
                name: "t_e_ville_vil");

            migrationBuilder.DropTable(
                name: "t_e_client_cli");

            migrationBuilder.DropTable(
                name: "t_e_pays_pay");

            migrationBuilder.DropTable(
                name: "t_e_typeproduit_tpd");

            migrationBuilder.DropTable(
                name: "t_e_categorieproduit_cat");

            migrationBuilder.DropTable(
                name: "t_e_photo_pho");

            migrationBuilder.DropSequence(
                name: "seq_adr");

            migrationBuilder.DropSequence(
                name: "seq_avi");

            migrationBuilder.DropSequence(
                name: "seq_car");

            migrationBuilder.DropSequence(
                name: "seq_cli");

            migrationBuilder.DropSequence(
                name: "seq_cmd");

            migrationBuilder.DropSequence(
                name: "seq_msg");

            migrationBuilder.DropSequence(
                name: "seq_pho");

            migrationBuilder.DropSequence(
                name: "seq_pmt");

            migrationBuilder.DropSequence(
                name: "seq_prd");

            migrationBuilder.DropSequence(
                name: "seq_sga");
        }
    }
}
