using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coboss.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employers_EmployeeID",
                schema: "coboss",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                schema: "coboss",
                table: "Users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "coboss",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Term",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 19, 59, 1, 970, DateTimeKind.Utc).AddTicks(4016),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 19, 26, 33, 350, DateTimeKind.Utc).AddTicks(4320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 19, 59, 1, 970, DateTimeKind.Utc).AddTicks(3785),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 19, 26, 33, 350, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "coboss",
                table: "BusinnessTaskRealisations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTaskRealisations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 19, 59, 1, 970, DateTimeKind.Utc).AddTicks(5623),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 19, 26, 33, 350, DateTimeKind.Utc).AddTicks(5973));

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employers_EmployeeID",
                schema: "coboss",
                table: "Users",
                column: "EmployeeID",
                principalSchema: "coboss",
                principalTable: "Employers",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employers_EmployeeID",
                schema: "coboss",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                schema: "coboss",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "coboss",
                table: "Users",
                type: "text",
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
                defaultValue: new DateTime(2023, 10, 25, 19, 26, 33, 350, DateTimeKind.Utc).AddTicks(4320),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 19, 59, 1, 970, DateTimeKind.Utc).AddTicks(4016));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 19, 26, 33, 350, DateTimeKind.Utc).AddTicks(4124),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 19, 59, 1, 970, DateTimeKind.Utc).AddTicks(3785));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "coboss",
                table: "BusinnessTaskRealisations",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "coboss",
                table: "BusinnessTaskRealisations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 10, 25, 19, 26, 33, 350, DateTimeKind.Utc).AddTicks(5973),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 10, 25, 19, 59, 1, 970, DateTimeKind.Utc).AddTicks(5623));

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employers_EmployeeID",
                schema: "coboss",
                table: "Users",
                column: "EmployeeID",
                principalSchema: "coboss",
                principalTable: "Employers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
