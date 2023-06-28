using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunGroops.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImageURLChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePictureURL",
                table: "Clubs",
                newName: "ImageURL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "Clubs",
                newName: "ProfilePictureURL");
        }
    }
}
