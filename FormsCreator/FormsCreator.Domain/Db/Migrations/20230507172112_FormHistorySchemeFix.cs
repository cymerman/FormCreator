using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsCreator.Domain.Db.Migrations
{
    public partial class FormHistorySchemeFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_form_history_users_user_id",
                table: "form_history");

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

            migrationBuilder.AddForeignKey(
                name: "fk_form_history_users_user_id",
                table: "form_history",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_form_history_users_user_id",
                table: "form_history");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTime(2023, 5, 7, 17, 15, 59, 880, DateTimeKind.Local).AddTicks(5324), new DateTime(2023, 5, 7, 17, 15, 59, 883, DateTimeKind.Local).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTime(2023, 5, 7, 17, 15, 59, 883, DateTimeKind.Local).AddTicks(5422), new DateTime(2023, 5, 7, 17, 15, 59, 883, DateTimeKind.Local).AddTicks(5428) });

            migrationBuilder.AddForeignKey(
                name: "fk_form_history_users_user_id",
                table: "form_history",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
