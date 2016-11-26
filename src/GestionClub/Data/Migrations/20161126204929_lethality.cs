using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GestionClub.Data.Migrations
{
    public partial class lethality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tournois",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Createur = table.Column<string>(nullable: true),
                    DateCreation = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Localisation = table.Column<string>(nullable: true),
                    Prix = table.Column<string>(nullable: true),
                    Start = table.Column<bool>(nullable: false),
                    State = table.Column<bool>(nullable: false),
                    Titre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournois", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateInscription = table.Column<DateTime>(nullable: false),
                    Etat = table.Column<bool>(nullable: false),
                    Nom = table.Column<string>(nullable: true),
                    NomUtilisateur = table.Column<string>(nullable: true),
                    Prénom = table.Column<string>(nullable: true),
                    TournoiID = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Participants_Tournois_TournoiID",
                        column: x => x.TournoiID,
                        principalTable: "Tournois",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateJouer = table.Column<DateTime>(nullable: false),
                    Etat = table.Column<bool>(nullable: false),
                    Gagnant = table.Column<bool>(nullable: false),
                    Numero = table.Column<string>(nullable: true),
                    TournoiId = table.Column<int>(nullable: false),
                    User1ID = table.Column<int>(nullable: true),
                    User2ID = table.Column<int>(nullable: true),
                    UserId1 = table.Column<string>(nullable: true),
                    UserId2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Parties_Tournois_TournoiId",
                        column: x => x.TournoiId,
                        principalTable: "Tournois",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parties_Participants_User1ID",
                        column: x => x.User1ID,
                        principalTable: "Participants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parties_Participants_User2ID",
                        column: x => x.User2ID,
                        principalTable: "Participants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_TournoiID",
                table: "Participants",
                column: "TournoiID");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_UserId",
                table: "Participants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_TournoiId",
                table: "Parties",
                column: "TournoiId");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_User1ID",
                table: "Parties",
                column: "User1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_User2ID",
                table: "Parties",
                column: "User2ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Tournois");
        }
    }
}
