using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterMinerV1.Migrations
{
    public partial class YetAnotherOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Players_playerId",
                table: "Links");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_Upgrades_upgradeId",
                table: "Links");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Players_playerId",
                table: "Links",
                column: "playerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Upgrades_upgradeId",
                table: "Links",
                column: "upgradeId",
                principalTable: "Upgrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Players_playerId",
                table: "Links");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_Upgrades_upgradeId",
                table: "Links");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Players_playerId",
                table: "Links",
                column: "playerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Upgrades_upgradeId",
                table: "Links",
                column: "upgradeId",
                principalTable: "Upgrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
