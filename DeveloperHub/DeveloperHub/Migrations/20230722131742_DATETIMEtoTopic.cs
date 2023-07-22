using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeveloperHub.Migrations
{
    /// <inheritdoc />
    public partial class DATETIMEtoTopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateTime",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Topics");
        }
    }
}
