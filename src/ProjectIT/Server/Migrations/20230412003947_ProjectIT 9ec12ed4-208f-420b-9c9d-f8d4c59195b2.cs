using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectIT.Server.Migrations
{
    /// <inheritdoc />
    public partial class ProjectIT9ec12ed4208f420b9c9df8d4c59195b2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");
        }
    }
}
