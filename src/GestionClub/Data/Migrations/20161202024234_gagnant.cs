using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionClub.Data.Migrations
{
    public partial class gagnant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<int>(
                name: "UserId2",
                table: "Parties",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "UserId1",
                table: "Parties",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "Numero",
                table: "Parties",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombrePartie",
                table: "Tournois");

            migrationBuilder.AlterColumn<string>(
                name: "UserId2",
                table: "Parties",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "Parties",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Parties",
                nullable: true);
        }
    }
}
