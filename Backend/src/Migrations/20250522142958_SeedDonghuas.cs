using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DonghuaFlix.Migrations
{
    /// <inheritdoc />
    public partial class SeedDonghuas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Donghuas",
                columns: new[] { "Id", "CreatedAt", "Genres", "Image", "ReleaseDate", "Sinopse", "Status", "Studio", "Title", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1de89ff8-1124-4ea5-83c6-bdcaa1d1e355"), new DateTime(2025, 5, 22, 14, 29, 56, 244, DateTimeKind.Utc).AddTicks(3216), 65568, "https://cdn.donghua.com/foghill.jpg", new DateTime(2020, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elementos e feras colidem em um mundo mitológico.", 3, "Samsara Studio", "Fog Hill of Five Elements", 3, null },
                    { new Guid("3bdb6119-fbe4-4e3e-a4bb-62d06b6871cc"), new DateTime(2025, 5, 22, 14, 29, 56, 244, DateTimeKind.Utc).AddTicks(3221), 196608, "https://cdn.donghua.com/bigfish.jpg", new DateTime(2016, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "A alma de uma jovem navega entre o mundo humano e o espiritual.", 2, "B&T Studio", "Big Fish & Begonia", 2, null },
                    { new Guid("64d7f030-2fd4-4070-9753-1802fc4523ec"), new DateTime(2025, 5, 22, 14, 29, 56, 244, DateTimeKind.Utc).AddTicks(1366), 16416, "https://cdn.donghua.com/kings-avatar.jpg", new DateTime(2017, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Um jogador profissional é forçado a sair do e-sport e começa de novo.", 2, "G.CMay Animation", "The King's Avatar", 1, null },
                    { new Guid("6b2c6cf0-d172-434f-878c-32f6a0842282"), new DateTime(2025, 5, 22, 14, 29, 56, 244, DateTimeKind.Utc).AddTicks(3218), 32896, "https://cdn.donghua.com/immortalking.jpg", new DateTime(2020, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Um prodígio espiritual tenta levar uma vida normal.", 1, "Haoliners Animation", "The Daily Life of the Immortal King", 1, null },
                    { new Guid("a88bc6c5-f848-4fd5-a1e8-bbe7aa5a7e41"), new DateTime(2025, 5, 22, 14, 29, 56, 244, DateTimeKind.Utc).AddTicks(3205), 18, "https://cdn.donghua.com/tgcf.jpg", new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Um deus exilado e um fantasma poderoso cruzam caminhos.", 1, "Haoliners Animation", "Heaven Official's Blessing", 1, null },
                    { new Guid("ab9d49a3-2e61-4c28-9aa9-2d0c91357df6"), new DateTime(2025, 5, 22, 14, 29, 56, 244, DateTimeKind.Utc).AddTicks(3227), 65540, "https://cdn.donghua.com/greatlord.jpg", new DateTime(2021, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Um jovem órfão com poderes místicos luta para proteger sua irmã.", 1, "Big Firebird", "Spare Me, Great Lord!", 1, null },
                    { new Guid("c1270c8d-2e84-4f62-98ce-ef57c018e285"), new DateTime(2025, 5, 22, 14, 29, 56, 244, DateTimeKind.Utc).AddTicks(3213), 96, "https://cdn.donghua.com/whitecat.jpg", new DateTime(2020, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Um oficial imperial felino investiga conspirações.", 1, "Nice Boat Animation", "White Cat Legend", 1, null },
                    { new Guid("d2a14412-b1e2-42c9-b7e0-4cd632df9dc8"), new DateTime(2025, 5, 22, 14, 29, 56, 244, DateTimeKind.Utc).AddTicks(3208), 160, "https://cdn.donghua.com/scissor7.jpg", new DateTime(2018, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Um barbeiro com amnésia atua como assassino fracassado.", 1, "AHA Entertainment", "Scissor Seven", 1, null },
                    { new Guid("d5e6fb84-82f1-4950-bbee-2677d059d61c"), new DateTime(2025, 5, 22, 14, 29, 56, 244, DateTimeKind.Utc).AddTicks(3223), 32800, "https://cdn.donghua.com/rakshasa.jpg", new DateTime(2016, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Espíritos protetores defendem os vivos dos mortos.", 2, "L2 Studio", "Rakshasa Street", 1, null },
                    { new Guid("f59fa054-16fc-4ad7-9ec7-26308bb9d774"), new DateTime(2025, 5, 22, 14, 29, 56, 244, DateTimeKind.Utc).AddTicks(3200), 32800, "https://cdn.donghua.com/modaozushi.jpg", new DateTime(2018, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cultivadores espirituais enfrentam antigos segredos e traições.", 2, "Tencent Penguin Pictures", "Mo Dao Zu Shi", 1, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("1de89ff8-1124-4ea5-83c6-bdcaa1d1e355"));

            migrationBuilder.DeleteData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("3bdb6119-fbe4-4e3e-a4bb-62d06b6871cc"));

            migrationBuilder.DeleteData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("64d7f030-2fd4-4070-9753-1802fc4523ec"));

            migrationBuilder.DeleteData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("6b2c6cf0-d172-434f-878c-32f6a0842282"));

            migrationBuilder.DeleteData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("a88bc6c5-f848-4fd5-a1e8-bbe7aa5a7e41"));

            migrationBuilder.DeleteData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("ab9d49a3-2e61-4c28-9aa9-2d0c91357df6"));

            migrationBuilder.DeleteData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("c1270c8d-2e84-4f62-98ce-ef57c018e285"));

            migrationBuilder.DeleteData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("d2a14412-b1e2-42c9-b7e0-4cd632df9dc8"));

            migrationBuilder.DeleteData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("d5e6fb84-82f1-4950-bbee-2677d059d61c"));

            migrationBuilder.DeleteData(
                table: "Donghuas",
                keyColumn: "Id",
                keyValue: new Guid("f59fa054-16fc-4ad7-9ec7-26308bb9d774"));
        }
    }
}
