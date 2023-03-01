using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoCycling.Migrations
{
    /// <inheritdoc />
    public partial class addedUserToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TokenId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tokentype = table.Column<string>(name: "token_type", type: "TEXT", nullable: false),
                    accesstoken = table.Column<string>(name: "access_token", type: "TEXT", nullable: false),
                    expiresat = table.Column<long>(name: "expires_at", type: "INTEGER", nullable: false),
                    refreshtoken = table.Column<string>(name: "refresh_token", type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_TokenId",
                table: "Users",
                column: "TokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTokens_TokenId",
                table: "Users",
                column: "TokenId",
                principalTable: "UserTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTokens_TokenId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropIndex(
                name: "IX_Users_TokenId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "Users");
        }
    }
}
