using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FormsCreator.Domain.Db.Migrations
{
    public partial class FormDefinition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_form_users_user_id",
                table: "form");

            migrationBuilder.DropPrimaryKey(
                name: "pk_form_definition",
                table: "form_definition");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "form");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "form");

            migrationBuilder.DropColumn(
                name: "modified_on",
                table: "form");

            migrationBuilder.RenameTable(
                name: "form_definition",
                newName: "form_definitions");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "form",
                newName: "form_definition_id");

            migrationBuilder.RenameIndex(
                name: "ix_form_user_id",
                table: "form",
                newName: "ix_form_form_definition_id");

            migrationBuilder.AddColumn<int>(
                name: "file_count",
                table: "form_definitions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "file_count_maximum",
                table: "form_definitions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "file_count_minimum",
                table: "form_definitions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "form_definition_id",
                table: "form_definitions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_file_count_limited",
                table: "form_definitions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "form_definitions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "user_id",
                table: "form_definitions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "pk_form_definitions",
                table: "form_definitions",
                column: "id");

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

            migrationBuilder.CreateIndex(
                name: "ix_form_definitions_form_definition_id",
                table: "form_definitions",
                column: "form_definition_id");

            migrationBuilder.CreateIndex(
                name: "ix_form_definitions_user_id",
                table: "form_definitions",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_form_form_definitions_form_definition_id",
                table: "form",
                column: "form_definition_id",
                principalTable: "form_definitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_form_definitions_form_definitions_form_definition_id",
                table: "form_definitions",
                column: "form_definition_id",
                principalTable: "form_definitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_form_definitions_users_user_id",
                table: "form_definitions",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_form_form_definitions_form_definition_id",
                table: "form");

            migrationBuilder.DropForeignKey(
                name: "fk_form_definitions_form_definitions_form_definition_id",
                table: "form_definitions");

            migrationBuilder.DropForeignKey(
                name: "fk_form_definitions_users_user_id",
                table: "form_definitions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_form_definitions",
                table: "form_definitions");

            migrationBuilder.DropIndex(
                name: "ix_form_definitions_form_definition_id",
                table: "form_definitions");

            migrationBuilder.DropIndex(
                name: "ix_form_definitions_user_id",
                table: "form_definitions");

            migrationBuilder.DropColumn(
                name: "file_count",
                table: "form_definitions");

            migrationBuilder.DropColumn(
                name: "file_count_maximum",
                table: "form_definitions");

            migrationBuilder.DropColumn(
                name: "file_count_minimum",
                table: "form_definitions");

            migrationBuilder.DropColumn(
                name: "form_definition_id",
                table: "form_definitions");

            migrationBuilder.DropColumn(
                name: "is_file_count_limited",
                table: "form_definitions");

            migrationBuilder.DropColumn(
                name: "status",
                table: "form_definitions");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "form_definitions");

            migrationBuilder.RenameTable(
                name: "form_definitions",
                newName: "form_definition");

            migrationBuilder.RenameColumn(
                name: "form_definition_id",
                table: "form",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_form_form_definition_id",
                table: "form",
                newName: "ix_form_user_id");

            migrationBuilder.AddColumn<long>(
                name: "created_by",
                table: "form",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "modified_by",
                table: "form",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_on",
                table: "form",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "pk_form_definition",
                table: "form_definition",
                column: "id");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTime(2022, 4, 18, 18, 4, 47, 199, DateTimeKind.Local).AddTicks(3809), new DateTime(2022, 4, 18, 18, 4, 47, 201, DateTimeKind.Local).AddTicks(3886) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_on", "modified_on" },
                values: new object[] { new DateTime(2022, 4, 18, 18, 4, 47, 201, DateTimeKind.Local).AddTicks(4117), new DateTime(2022, 4, 18, 18, 4, 47, 201, DateTimeKind.Local).AddTicks(4125) });

            migrationBuilder.AddForeignKey(
                name: "fk_form_users_user_id",
                table: "form",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
