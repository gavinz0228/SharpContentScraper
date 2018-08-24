using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LyricsScraping.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Singer",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_Singer_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Lyrics",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: false),
                    Singer = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DownloadTime = table.Column<DateTime>(nullable: false),
                    SingerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_Lyrics_Title", x => x.Title);
                    table.ForeignKey(
                        name: "FK_Lyrics_Singer_SingerName",
                        column: x => x.SingerName,
                        principalTable: "Singer",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lyrics_SingerName",
                table: "Lyrics",
                column: "SingerName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lyrics");

            migrationBuilder.DropTable(
                name: "Singer");
        }
    }
}
