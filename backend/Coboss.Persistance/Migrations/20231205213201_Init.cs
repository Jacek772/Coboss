using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coboss.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "coboss");

            migrationBuilder.CreateTable(
                name: "Attachments",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TableName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ParentID = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employers",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PESEL = table.Column<string>(type: "text", nullable: true),
                    NIP = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlobalSettings",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    DateTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999)),
                    CostHourOfWork = table.Column<decimal>(type: "numeric", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeHistories_Employers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "coboss",
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Term = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 12, 5, 21, 32, 1, 884, DateTimeKind.Utc).AddTicks(7541)),
                    ManagerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Employers_ManagerId",
                        column: x => x.ManagerId,
                        principalSchema: "coboss",
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Salt = table.Column<byte[]>(type: "bytea", maxLength: 255, nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Employers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "coboss",
                        principalTable: "Employers",
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 12, 5, 21, 32, 1, 883, DateTimeKind.Utc).AddTicks(9094)),
                    Term = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 12, 5, 21, 32, 1, 883, DateTimeKind.Utc).AddTicks(9268)),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinnessTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinnessTasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "coboss",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokensData",
                schema: "coboss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "character varying(88)", maxLength: 88, nullable: false),
                    JwtId = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 12, 5, 21, 32, 1, 884, DateTimeKind.Utc).AddTicks(8929)),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Used = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 12, 5, 21, 32, 1, 883, DateTimeKind.Utc).AddTicks(7688)),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BusinnessTaskId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinnessTaskComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinnessTaskComments_BusinnessTasks_BusinnessTaskId",
                        column: x => x.BusinnessTaskId,
                        principalSchema: "coboss",
                        principalTable: "BusinnessTasks",
                        principalColumn: "Id");
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 12, 5, 21, 32, 1, 884, DateTimeKind.Utc).AddTicks(2210)),
                    TimeSpan = table.Column<TimeSpan>(type: "interval", nullable: false, defaultValue: new TimeSpan(0, 0, 0, 0, 0)),
                    Description = table.Column<string>(type: "text", nullable: true),
                    BusinnessTaskId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinnessTaskRealisations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinnessTaskRealisations_BusinnessTasks_BusinnessTaskId",
                        column: x => x.BusinnessTaskId,
                        principalSchema: "coboss",
                        principalTable: "BusinnessTasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_TableName_ParentID",
                schema: "coboss",
                table: "Attachments",
                columns: new[] { "TableName", "ParentID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinnessTaskComments_BusinnessTaskId",
                schema: "coboss",
                table: "BusinnessTaskComments",
                column: "BusinnessTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinnessTaskComments_UserId",
                schema: "coboss",
                table: "BusinnessTaskComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinnessTaskRealisations_BusinnessTaskId",
                schema: "coboss",
                table: "BusinnessTaskRealisations",
                column: "BusinnessTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinnessTasks_ProjectId",
                schema: "coboss",
                table: "BusinnessTasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistories_EmployeeId",
                schema: "coboss",
                table: "EmployeeHistories",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_NIP",
                schema: "coboss",
                table: "Employers",
                column: "NIP",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employers_PESEL",
                schema: "coboss",
                table: "Employers",
                column: "PESEL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GlobalSettings_Key",
                schema: "coboss",
                table: "GlobalSettings",
                column: "Key",
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
                name: "Attachments",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "BusinnessTaskComments",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "BusinnessTaskRealisations",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "EmployeeHistories",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "GlobalSettings",
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
                name: "Employers",
                schema: "coboss");
        }
    }
}
