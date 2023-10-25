using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                name: "Employers",
                schema: "coboss",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "coboss",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "coboss",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Salt = table.Column<byte[]>(type: "bytea", maxLength: 255, nullable: false),
                    EmployeeID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Employers_EmployeeID",
                        column: x => x.EmployeeID,
                        principalSchema: "coboss",
                        principalTable: "Employers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinnessTasks",
                schema: "coboss",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 10, 22, 22, 58, 3, 773, DateTimeKind.Utc).AddTicks(6415)),
                    Term = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 10, 22, 22, 58, 3, 773, DateTimeKind.Utc).AddTicks(6605)),
                    ProjectID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinnessTasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BusinnessTasks_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "coboss",
                        principalTable: "Projects",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                schema: "coboss",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    BusinnessTaskID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Attachments_BusinnessTasks_BusinnessTaskID",
                        column: x => x.BusinnessTaskID,
                        principalSchema: "coboss",
                        principalTable: "BusinnessTasks",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "BusinnessTaskRealisations",
                schema: "coboss",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2023, 10, 22, 22, 58, 3, 773, DateTimeKind.Utc).AddTicks(8419)),
                    TimeSpan = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    BusinnessTaskID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinnessTaskRealisations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BusinnessTaskRealisations_BusinnessTasks_BusinnessTaskID",
                        column: x => x.BusinnessTaskID,
                        principalSchema: "coboss",
                        principalTable: "BusinnessTasks",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_BusinnessTaskID",
                schema: "coboss",
                table: "Attachments",
                column: "BusinnessTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinnessTaskRealisations_BusinnessTaskID",
                schema: "coboss",
                table: "BusinnessTaskRealisations",
                column: "BusinnessTaskID");

            migrationBuilder.CreateIndex(
                name: "IX_BusinnessTasks_ProjectID",
                schema: "coboss",
                table: "BusinnessTasks",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeID",
                schema: "coboss",
                table: "Users",
                column: "EmployeeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                schema: "coboss",
                table: "Users",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "BusinnessTaskRealisations",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "BusinnessTasks",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "Employers",
                schema: "coboss");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "coboss");
        }
    }
}
