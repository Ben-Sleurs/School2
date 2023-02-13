using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PXLFilmzaal.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilmImages",
                columns: table => new
                {
                    FilmImageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FilmImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FilmImageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmImages", x => x.FilmImageId);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    FilmId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FilmNaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilmImageId = table.Column<int>(type: "int", nullable: true),
                    FilmImageId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.FilmId);
                    table.ForeignKey(
                        name: "FK_Films_FilmImages_FilmImageId1",
                        column: x => x.FilmImageId1,
                        principalTable: "FilmImages",
                        principalColumn: "FilmImageId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Films_FilmImageId1",
                table: "Films",
                column: "FilmImageId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "FilmImages");
        }
    }
}
