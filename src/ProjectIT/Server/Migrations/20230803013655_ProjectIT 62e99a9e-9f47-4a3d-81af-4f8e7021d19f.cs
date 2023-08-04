using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectIT.Server.Migrations
{
    /// <inheritdoc />
    public partial class ProjectIT62e99a9e9f474a3d81af4f8e7021d19f : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestStudent_Student_MembersId",
                table: "RequestStudent");

            migrationBuilder.RenameColumn(
                name: "MembersId",
                table: "RequestStudent",
                newName: "ExtraMembersId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Request",
                newName: "DescriptionHtml");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Request",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Request",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Request_StudentId",
                table: "Request",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Student_StudentId",
                table: "Request",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStudent_Student_ExtraMembersId",
                table: "RequestStudent",
                column: "ExtraMembersId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Student_StudentId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestStudent_Student_ExtraMembersId",
                table: "RequestStudent");

            migrationBuilder.DropIndex(
                name: "IX_Request_StudentId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Request");

            migrationBuilder.RenameColumn(
                name: "ExtraMembersId",
                table: "RequestStudent",
                newName: "MembersId");

            migrationBuilder.RenameColumn(
                name: "DescriptionHtml",
                table: "Request",
                newName: "Description");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStudent_Student_MembersId",
                table: "RequestStudent",
                column: "MembersId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
