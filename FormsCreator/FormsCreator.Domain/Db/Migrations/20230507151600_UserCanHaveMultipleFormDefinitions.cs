using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsCreator.Domain.Db.Migrations
{
    public partial class UserCanHaveMultipleFormDefinitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_form_definitions_users_user_id",
                table: "form_definitions");

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
                name: "fk_form_definitions_users_user_id",
                table: "form_definitions",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_form_definitions_users_user_id",
                table: "form_definitions");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTime(2023, 5, 7, 15, 39, 35, 739, DateTimeKind.Local).AddTicks(2660), new DateTime(2023, 5, 7, 15, 39, 35, 741, DateTimeKind.Local).AddTicks(4678) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTime(2023, 5, 7, 15, 39, 35, 741, DateTimeKind.Local).AddTicks(4902), new DateTime(2023, 5, 7, 15, 39, 35, 741, DateTimeKind.Local).AddTicks(4907) });

            migrationBuilder.AddForeignKey(
                name: "fk_form_definitions_users_user_id",
                table: "form_definitions",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
