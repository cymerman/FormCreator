using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsCreator.Domain.Db.Migrations
{
    public partial class FormSchemFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "data",
                table: "form",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTime(2023, 5, 21, 13, 22, 6, 513, DateTimeKind.Local).AddTicks(4641), new DateTime(2023, 5, 21, 13, 22, 6, 530, DateTimeKind.Local).AddTicks(343) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTime(2023, 5, 21, 13, 22, 6, 530, DateTimeKind.Local).AddTicks(2954), new DateTime(2023, 5, 21, 13, 22, 6, 530, DateTimeKind.Local).AddTicks(3023) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "data",
                table: "form",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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
    }
}
