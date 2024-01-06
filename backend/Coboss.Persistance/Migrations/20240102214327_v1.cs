using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coboss.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeCodes_CodeNumber",
                schema: "coboss",
                table: "EmployeeCodes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "coboss",
                table: "RefreshTokensData",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 2, 21, 43, 27, 875, DateTimeKind.Utc).AddTicks(6201),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 1, 2, 21, 38, 44, 282, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Term",
                schema: "coboss",
                table: "Projects",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 2, 21, 43, 27, 875, DateTimeKind.Utc).AddTicks(4856),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 1, 2, 21, 38, 44, 282, DateTimeKind.Utc).AddTicks(3516));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Term",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 2, 21, 43, 27, 874, DateTimeKind.Utc).AddTicks(6215),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 1, 2, 21, 38, 44, 281, DateTimeKind.Utc).AddTicks(3736));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 2, 21, 43, 27, 874, DateTimeKind.Utc).AddTicks(5998),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 1, 2, 21, 38, 44, 281, DateTimeKind.Utc).AddTicks(3552));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTaskRealisations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 2, 21, 43, 27, 874, DateTimeKind.Utc).AddTicks(8878),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 1, 2, 21, 38, 44, 281, DateTimeKind.Utc).AddTicks(6508));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTaskComments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 2, 21, 43, 27, 874, DateTimeKind.Utc).AddTicks(4731),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 1, 2, 21, 38, 44, 281, DateTimeKind.Utc).AddTicks(2194));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCodes_EntityName_CodeNumber",
                schema: "coboss",
                table: "EmployeeCodes",
                columns: new[] { "EntityName", "CodeNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeCodes_EntityName_CodeNumber",
                schema: "coboss",
                table: "EmployeeCodes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                schema: "coboss",
                table: "RefreshTokensData",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 2, 21, 38, 44, 282, DateTimeKind.Utc).AddTicks(5370),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 1, 2, 21, 43, 27, 875, DateTimeKind.Utc).AddTicks(6201));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Term",
                schema: "coboss",
                table: "Projects",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 2, 21, 38, 44, 282, DateTimeKind.Utc).AddTicks(3516),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 1, 2, 21, 43, 27, 875, DateTimeKind.Utc).AddTicks(4856));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Term",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 2, 21, 38, 44, 281, DateTimeKind.Utc).AddTicks(3736),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 1, 2, 21, 43, 27, 874, DateTimeKind.Utc).AddTicks(6215));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 2, 21, 38, 44, 281, DateTimeKind.Utc).AddTicks(3552),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 1, 2, 21, 43, 27, 874, DateTimeKind.Utc).AddTicks(5998));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTaskRealisations",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 2, 21, 38, 44, 281, DateTimeKind.Utc).AddTicks(6508),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 1, 2, 21, 43, 27, 874, DateTimeKind.Utc).AddTicks(8878));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTaskComments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 1, 2, 21, 38, 44, 281, DateTimeKind.Utc).AddTicks(2194),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2024, 1, 2, 21, 43, 27, 874, DateTimeKind.Utc).AddTicks(4731));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCodes_CodeNumber",
                schema: "coboss",
                table: "EmployeeCodes",
                column: "CodeNumber",
                unique: true);
        }
    }
}
