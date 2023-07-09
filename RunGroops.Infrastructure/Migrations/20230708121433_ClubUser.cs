using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunGroops.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClubUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserClub");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Clubs",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_AppUserId",
                table: "Clubs",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_AspNetUsers_AppUserId",
                table: "Clubs",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_AspNetUsers_AppUserId",
                table: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_AppUserId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Clubs");

            migrationBuilder.CreateTable(
                name: "AppUserClub",
                columns: table => new
                {
                    ClubsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClub", x => new { x.ClubsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AppUserClub_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserClub_Clubs_ClubsId",
                        column: x => x.ClubsId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserClub_UsersId",
                table: "AppUserClub",
                column: "UsersId");
        }
    }
}
