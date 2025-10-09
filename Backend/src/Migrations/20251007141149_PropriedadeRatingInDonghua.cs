using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonghuaFlix.Migrations
{
    /// <inheritdoc />
    public partial class PropriedadeRatingInDonghua : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Donghuas",
                type: "REAL",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("1de89ff8-1124-4ea5-83c6-bdcaa1d1e355"),
                column: "Rating",
                value: 10f);

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("3bdb6119-fbe4-4e3e-a4bb-62d06b6871cc"),
                column: "Rating",
                value: 10f);

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("64d7f030-2fd4-4070-9753-1802fc4523ec"),
                column: "Rating",
                value: 10f);

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("6b2c6cf0-d172-434f-878c-32f6a0842282"),
                column: "Rating",
                value: 10f);

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("a88bc6c5-f848-4fd5-a1e8-bbe7aa5a7e41"),
                column: "Rating",
                value: 10f);

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("ab9d49a3-2e61-4c28-9aa9-2d0c91357df6"),
                column: "Rating",
                value: 10f);

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("c1270c8d-2e84-4f62-98ce-ef57c018e285"),
                column: "Rating",
                value: 10f);

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("d2a14412-b1e2-42c9-b7e0-4cd632df9dc8"),
                column: "Rating",
                value: 10f);

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("d5e6fb84-82f1-4950-bbee-2677d059d61c"),
                column: "Rating",
                value: 10f);

            migrationBuilder.UpdateData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("f59fa054-16fc-4ad7-9ec7-26308bb9d774"),
                column: "Rating",
                value: 10f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Donghuas");
        }
    }
}
