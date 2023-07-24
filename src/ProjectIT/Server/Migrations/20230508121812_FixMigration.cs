using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectIT.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectStudent_Project_ProjectsId",
                table: "ProjectStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestSupervisor_Request_RequestsId",
                table: "RequestSupervisor");

            migrationBuilder.RenameColumn(
                name: "RequestsId",
                table: "RequestSupervisor",
                newName: "ReceivedRequestsId");

            migrationBuilder.RenameColumn(
                name: "ProjectsId",
                table: "ProjectStudent",
                newName: "AppliedProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectStudent_Project_AppliedProjectsId",
                table: "ProjectStudent",
                column: "AppliedProjectsId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestSupervisor_Request_ReceivedRequestsId",
                table: "RequestSupervisor",
                column: "ReceivedRequestsId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectStudent_Project_AppliedProjectsId",
                table: "ProjectStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestSupervisor_Request_ReceivedRequestsId",
                table: "RequestSupervisor");

            migrationBuilder.RenameColumn(
                name: "ReceivedRequestsId",
                table: "RequestSupervisor",
                newName: "RequestsId");

            migrationBuilder.RenameColumn(
                name: "AppliedProjectsId",
                table: "ProjectStudent",
                newName: "ProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectStudent_Project_ProjectsId",
                table: "ProjectStudent",
                column: "ProjectsId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestSupervisor_Request_RequestsId",
                table: "RequestSupervisor",
                column: "RequestsId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
