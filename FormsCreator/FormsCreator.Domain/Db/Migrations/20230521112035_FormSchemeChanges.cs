using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsCreator.Domain.Db.Migrations
{
    public partial class FormSchemeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "form");

            migrationBuilder.RenameColumn(
                name: "html_form",
                table: "form",
                newName: "data");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTime(2023, 5, 21, 13, 20, 32, 215, DateTimeKind.Local).AddTicks(5349), new DateTime(2023, 5, 21, 13, 20, 32, 231, DateTimeKind.Local).AddTicks(9052) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTime(2023, 5, 21, 13, 20, 32, 232, DateTimeKind.Local).AddTicks(1331), new DateTime(2023, 5, 21, 13, 20, 32, 232, DateTimeKind.Local).AddTicks(1402) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "data",
                table: "form",
                newName: "html_form");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "form",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTime(2023, 5, 14, 22, 18, 20, 989, DateTimeKind.Local).AddTicks(9629), new DateTime(2023, 5, 14, 22, 18, 20, 992, DateTimeKind.Local).AddTicks(876) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTime(2023, 5, 14, 22, 18, 20, 992, DateTimeKind.Local).AddTicks(1100), new DateTime(2023, 5, 14, 22, 18, 20, 992, DateTimeKind.Local).AddTicks(1105) });
        }
    }
}
