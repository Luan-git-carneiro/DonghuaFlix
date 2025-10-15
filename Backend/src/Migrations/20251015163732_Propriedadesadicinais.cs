using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonghuaFlix.Migrations
{
    /// <inheritdoc />
    public partial class Propriedadesadicinais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Episodes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "Episodes",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Episodes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Episodes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Sinopse",
                table: "Donghuas",
                type: "TEXT",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Donghuas",
                type: "TEXT",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Banner",
                table: "Donghuas",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Donghuas",
                type: "TEXT",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEnglish",
                table: "Donghuas",
                type: "TEXT",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trailer",
                table: "Donghuas",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("1de89ff8-1124-4ea5-83c6-bdcaa1d1e355"),
                columns: new[] { "Banner", "Description", "TitleEnglish", "Trailer" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("3bdb6119-fbe4-4e3e-a4bb-62d06b6871cc"),
                columns: new[] { "Banner", "Description", "TitleEnglish", "Trailer" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("64d7f030-2fd4-4070-9753-1802fc4523ec"),
                columns: new[] { "Banner", "Description", "TitleEnglish", "Trailer" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("6b2c6cf0-d172-434f-878c-32f6a0842282"),
                columns: new[] { "Banner", "Description", "TitleEnglish", "Trailer" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("a88bc6c5-f848-4fd5-a1e8-bbe7aa5a7e41"),
                columns: new[] { "Banner", "Description", "TitleEnglish", "Trailer" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("ab9d49a3-2e61-4c28-9aa9-2d0c91357df6"),
                columns: new[] { "Banner", "Description", "TitleEnglish", "Trailer" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("c1270c8d-2e84-4f62-98ce-ef57c018e285"),
                columns: new[] { "Banner", "Description", "TitleEnglish", "Trailer" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("d2a14412-b1e2-42c9-b7e0-4cd632df9dc8"),
                columns: new[] { "Banner", "Description", "TitleEnglish", "Trailer" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("d5e6fb84-82f1-4950-bbee-2677d059d61c"),
                columns: new[] { "Banner", "Description", "TitleEnglish", "Trailer" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("f59fa054-16fc-4ad7-9ec7-26308bb9d774"),
                columns: new[] { "Banner", "Description", "TitleEnglish", "Trailer" },
                values: new object[] { null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "Banner",
                table: "Donghuas");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Donghuas");

            migrationBuilder.DropColumn(
                name: "TitleEnglish",
                table: "Donghuas");

            migrationBuilder.DropColumn(
                name: "Trailer",
                table: "Donghuas");

            migrationBuilder.AlterColumn<string>(
                name: "Sinopse",
                table: "Donghuas",
                type: "TEXT",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Donghuas",
                type: "TEXT",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500);
        }
    }
}
