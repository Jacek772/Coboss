using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coboss.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "coboss",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Term",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 20, 7, 29, 884, DateTimeKind.Utc).AddTicks(8476),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 19, 59, 1, 970, DateTimeKind.Utc).AddTicks(4016));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 20, 7, 29, 884, DateTimeKind.Utc).AddTicks(8236),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 19, 59, 1, 970, DateTimeKind.Utc).AddTicks(3785));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTaskRealisations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 20, 7, 29, 885, DateTimeKind.Utc).AddTicks(143),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 19, 59, 1, 970, DateTimeKind.Utc).AddTicks(5623));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "coboss",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Term",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 19, 59, 1, 970, DateTimeKind.Utc).AddTicks(4016),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 20, 7, 29, 884, DateTimeKind.Utc).AddTicks(8476));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 19, 59, 1, 970, DateTimeKind.Utc).AddTicks(3785),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 20, 7, 29, 884, DateTimeKind.Utc).AddTicks(8236));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTaskRealisations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 19, 59, 1, 970, DateTimeKind.Utc).AddTicks(5623),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 20, 7, 29, 885, DateTimeKind.Utc).AddTicks(143));
        }
    }
}
