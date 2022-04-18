using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsCreator.Domain.Db.Migrations
{
    public partial class FormUId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "form_uid",
                table: "form_definitions",
                type: "text",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "form_uid",
                table: "form_definitions");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTime(2023, 5, 7, 19, 21, 12, 4, DateTimeKind.Local).AddTicks(6061), new DateTime(2023, 5, 7, 19, 21, 12, 6, DateTimeKind.Local).AddTicks(7864) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTime(2023, 5, 7, 19, 21, 12, 6, DateTimeKind.Local).AddTicks(8087), new DateTime(2023, 5, 7, 19, 21, 12, 6, DateTimeKind.Local).AddTicks(8093) });
        }
    }
}
