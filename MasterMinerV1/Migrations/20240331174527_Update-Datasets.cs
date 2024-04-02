using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterMinerV1.Migrations
{
    public partial class UpdateDatasets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "GameSlot",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameSlot",
                table: "Players",
                column: "GameSlot",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_GameSlot",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GameSlot",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
