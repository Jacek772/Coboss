using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Coboss.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "coboss");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PESEL = table.Column<string>(type: "text", nullable: true),
                    NIP = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlobalSettings",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectCodes",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntityName = table.Column<string>(type: "text", nullable: false),
                    CodeNumber = table.Column<int>(type: "integer", nullable: false),
                    CodeLength = table.Column<int>(type: "integer", nullable: false, defaultValue: 10)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHistories",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    DateTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999)),
                    CostHourOfWork = table.Column<decimal>(type: "numeric", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeHistories_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "coboss",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Term = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 2, 1, 20, 42, 31, 600, DateTimeKind.Utc).AddTicks(3622)),
                    ManagerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalSchema: "coboss",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Salt = table.Column<byte[]>(type: "bytea", maxLength: 255, nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "coboss",
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "coboss",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinnessTasks",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 2, 1, 20, 42, 31, 599, DateTimeKind.Utc).AddTicks(539)),
                    Term = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 2, 1, 20, 42, 31, 599, DateTimeKind.Utc).AddTicks(727)),
                    ProjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinnessTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinnessTasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "coboss",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokensData",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "character varying(88)", maxLength: 88, nullable: false),
                    JwtId = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 2, 1, 20, 42, 31, 600, DateTimeKind.Utc).AddTicks(5878)),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Used = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokensData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokensData_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "coboss",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinnessTaskComments",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 2, 1, 20, 42, 31, 598, DateTimeKind.Utc).AddTicks(9114)),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TaskId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinnessTaskComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinnessTaskComments_BusinnessTasks_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "coboss",
                        principalTable: "BusinnessTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinnessTaskComments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "coboss",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinnessTaskRealisations",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 2, 1, 20, 42, 31, 599, DateTimeKind.Utc).AddTicks(3231)),
                    TimeSpan = table.Column<TimeSpan>(type: "interval", nullable: false, defaultValue: new TimeSpan(0, 0, 0, 0, 0)),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinnessTaskRealisations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinnessTaskRealisations_BusinnessTasks_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "coboss",
                        principalTable: "BusinnessTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinnessTaskRealisations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "coboss",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinnessTasksUsers",
                schema: "coboss",
                columns: table => new
                {
                    BusinnessTaskId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinnessTasksUsers", x => new { x.BusinnessTaskId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_BusinnessTasksUsers_BusinnessTasks_BusinnessTaskId",
                        column: x => x.BusinnessTaskId,
                        principalSchema: "coboss",
                        principalTable: "BusinnessTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinnessTasksUsers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "coboss",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinnessTaskComments_TaskId",
                schema: "coboss",
                table: "BusinnessTaskComments",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinnessTaskComments_UserId",
                schema: "coboss",
                table: "BusinnessTaskComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinnessTaskRealisations_EmployeeId",
                schema: "coboss",
                table: "BusinnessTaskRealisations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinnessTaskRealisations_TaskId",
                schema: "coboss",
                table: "BusinnessTaskRealisations",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinnessTasks_ProjectId",
                schema: "coboss",
                table: "BusinnessTasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinnessTasksUsers_EmployeeId",
                schema: "coboss",
                table: "BusinnessTasksUsers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistories_EmployeeId",
                schema: "coboss",
                table: "EmployeeHistories",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalSettings_Key",
                schema: "coboss",
                table: "GlobalSettings",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ObjectCodes_EntityName_CodeNumber",
                schema: "coboss",
                table: "ObjectCodes",
                columns: new[] { "EntityName", "CodeNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                schema: "coboss",
                table: "Projects",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Number",
                schema: "coboss",
                table: "Projects",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokensData_Token",
                schema: "coboss",
                table: "RefreshTokensData",
                column: "Token");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokensData_UserId",
                schema: "coboss",
                table: "RefreshTokensData",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                schema: "coboss",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "coboss",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeId",
                schema: "coboss",
                table: "Users",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "coboss",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinnessTaskComments",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "BusinnessTaskRealisations",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "BusinnessTasksUsers",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "EmployeeHistories",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "GlobalSettings",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "ObjectCodes",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "RefreshTokensData",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "BusinnessTasks",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "coboss");
        }
    }
}
