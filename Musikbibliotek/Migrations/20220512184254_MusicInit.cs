using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Musikbibliotek.Migrations
{
    public partial class MusicInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SongLength = table.Column<TimeSpan>(type: "time", nullable: false),
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    ArtistName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "AC/DC" },
                    { 2, "KORN" },
                    { 3, "System Of A Down" },
                    { 4, "Linkin Park" },
                    { 5, "Iron Maiden" }
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "ArtistId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Power Up" },
                    { 2, 2, "Requiem" },
                    { 3, 3, "Mezmerize" },
                    { 4, 4, "Meteora" },
                    { 5, 5, "Powerslave" }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "AlbumId", "ArtistId", "ArtistName", "Name", "SongLength" },
                values: new object[,]
                {
                    { 1, 1, 1, "AC/DC", "Shot In The Dark", new TimeSpan(0, 0, 3, 3, 0) },
                    { 2, 2, 2, "KORN", "Forgotten", new TimeSpan(0, 0, 3, 10, 0) },
                    { 3, 3, 3, "System Of A Down", "B.Y.O.B", new TimeSpan(0, 0, 4, 9, 0) },
                    { 4, 4, 4, "Linkin Park", "Faint", new TimeSpan(0, 0, 2, 25, 0) },
                    { 5, 5, 5, "Iron Maiden", "Aces High", new TimeSpan(0, 0, 4, 19, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_ArtistId",
                table: "Albums",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_AlbumId",
                table: "Songs",
                column: "AlbumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Artists");
        }
    }
}
