using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoCycling.Migrations
{
    /// <inheritdoc />
    public partial class refactoreddb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TileConquers_Tiles_TileId",
                table: "TileConquers");

            migrationBuilder.DropTable(
                name: "Tiles");

            migrationBuilder.DropIndex(
                name: "IX_TileConquers_TileId",
                table: "TileConquers");

            migrationBuilder.RenameColumn(
                name: "TileId",
                table: "TileConquers",
                newName: "Y");

            migrationBuilder.AddColumn<int>(
                name: "X",
                table: "TileConquers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "X",
                table: "TileConquers");

            migrationBuilder.RenameColumn(
                name: "Y",
                table: "TileConquers",
                newName: "TileId");

            migrationBuilder.CreateTable(
                name: "Tiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    X = table.Column<int>(type: "INTEGER", nullable: false),
                    Y = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TileConquers_TileId",
                table: "TileConquers",
                column: "TileId");

            migrationBuilder.AddForeignKey(
                name: "FK_TileConquers_Tiles_TileId",
                table: "TileConquers",
                column: "TileId",
                principalTable: "Tiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
