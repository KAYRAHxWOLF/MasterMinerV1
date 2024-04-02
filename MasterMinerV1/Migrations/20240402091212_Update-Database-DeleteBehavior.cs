using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterMinerV1.Migrations
{
    public partial class UpdateDatabaseDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Players_playerId",
                table: "Links");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Players_playerId",
                table: "Links",
                column: "playerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Players_playerId",
                table: "Links");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Players_playerId",
                table: "Links",
                column: "playerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
