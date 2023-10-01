using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjectIT.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixModelRestrictions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Programme = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supervisor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Profession = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DescriptionHtml = table.Column<string>(type: "text", nullable: false),
                    Languages = table.Column<string>(type: "text", nullable: false),
                    Programmes = table.Column<string>(type: "text", nullable: false),
                    Ects = table.Column<string>(type: "text", nullable: false),
                    Semester = table.Column<string>(type: "text", nullable: false),
                    SupervisorId = table.Column<int>(type: "integer", nullable: false),
                    CoSupervisorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Supervisor_CoSupervisorId",
                        column: x => x.CoSupervisorId,
                        principalTable: "Supervisor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Project_Supervisor_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupervisorTopic",
                columns: table => new
                {
                    SupervisorId = table.Column<int>(type: "integer", nullable: false),
                    TopicsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorTopic", x => new { x.SupervisorId, x.TopicsId });
                    table.ForeignKey(
                        name: "FK_SupervisorTopic_Supervisor_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupervisorTopic_Topic_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStudent",
                columns: table => new
                {
                    AppliedProjectsId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStudent", x => new { x.AppliedProjectsId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_ProjectStudent_Project_AppliedProjectsId",
                        column: x => x.AppliedProjectsId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectStudent_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTopic",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    TopicsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTopic", x => new { x.ProjectId, x.TopicsId });
                    table.ForeignKey(
                        name: "FK_ProjectTopic_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTopic_Topic_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGroup_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DescriptionHtml = table.Column<string>(type: "text", nullable: false),
                    Languages = table.Column<string>(type: "text", nullable: false),
                    Programmes = table.Column<string>(type: "text", nullable: false),
                    StudentGroupId = table.Column<int>(type: "integer", nullable: false),
                    Ects = table.Column<string>(type: "text", nullable: false),
                    Semester = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_StudentGroup_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentStudentGroup",
                columns: table => new
                {
                    StudentGroupId = table.Column<int>(type: "integer", nullable: false),
                    StudentsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentStudentGroup", x => new { x.StudentGroupId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_StudentStudentGroup_StudentGroup_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentStudentGroup_Student_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestStudent",
                columns: table => new
                {
                    RequestsId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStudent", x => new { x.RequestsId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_RequestStudent_Request_RequestsId",
                        column: x => x.RequestsId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestStudent_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestSupervisor",
                columns: table => new
                {
                    ReceivedRequestsId = table.Column<int>(type: "integer", nullable: false),
                    SupervisorsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestSupervisor", x => new { x.ReceivedRequestsId, x.SupervisorsId });
                    table.ForeignKey(
                        name: "FK_RequestSupervisor_Request_ReceivedRequestsId",
                        column: x => x.ReceivedRequestsId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestSupervisor_Supervisor_SupervisorsId",
                        column: x => x.SupervisorsId,
                        principalTable: "Supervisor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestTopic",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "integer", nullable: false),
                    TopicsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTopic", x => new { x.RequestId, x.TopicsId });
                    table.ForeignKey(
                        name: "FK_RequestTopic_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestTopic_Topic_TopicsId",
                        column: x => x.TopicsId,
                        principalTable: "Topic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_CoSupervisorId",
                table: "Project",
                column: "CoSupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_SupervisorId",
                table: "Project",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStudent_StudentId",
                table: "ProjectStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTopic_TopicsId",
                table: "ProjectTopic",
                column: "TopicsId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_StudentGroupId",
                table: "Request",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStudent_StudentId",
                table: "RequestStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestSupervisor_SupervisorsId",
                table: "RequestSupervisor",
                column: "SupervisorsId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTopic_TopicsId",
                table: "RequestTopic",
                column: "TopicsId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroup_ProjectId",
                table: "StudentGroup",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentStudentGroup_StudentsId",
                table: "StudentStudentGroup",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorTopic_TopicsId",
                table: "SupervisorTopic",
                column: "TopicsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectStudent");

            migrationBuilder.DropTable(
                name: "ProjectTopic");

            migrationBuilder.DropTable(
                name: "RequestStudent");

            migrationBuilder.DropTable(
                name: "RequestSupervisor");

            migrationBuilder.DropTable(
                name: "RequestTopic");

            migrationBuilder.DropTable(
                name: "StudentStudentGroup");

            migrationBuilder.DropTable(
                name: "SupervisorTopic");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "StudentGroup");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Supervisor");
        }
    }
}
