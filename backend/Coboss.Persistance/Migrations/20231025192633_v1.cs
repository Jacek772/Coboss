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
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "coboss",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Term",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 19, 26, 33, 350, DateTimeKind.Utc).AddTicks(4320),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 22, 22, 58, 3, 773, DateTimeKind.Utc).AddTicks(6605));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 19, 26, 33, 350, DateTimeKind.Utc).AddTicks(4124),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 22, 22, 58, 3, 773, DateTimeKind.Utc).AddTicks(6415));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTaskRealisations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 19, 26, 33, 350, DateTimeKind.Utc).AddTicks(5973),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 22, 22, 58, 3, 773, DateTimeKind.Utc).AddTicks(8419));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "coboss",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Term",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 22, 58, 3, 773, DateTimeKind.Utc).AddTicks(6605),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 19, 26, 33, 350, DateTimeKind.Utc).AddTicks(4320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 22, 58, 3, 773, DateTimeKind.Utc).AddTicks(6415),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 19, 26, 33, 350, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTaskRealisations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 22, 22, 58, 3, 773, DateTimeKind.Utc).AddTicks(8419),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 19, 26, 33, 350, DateTimeKind.Utc).AddTicks(5973));
        }
    }
}
