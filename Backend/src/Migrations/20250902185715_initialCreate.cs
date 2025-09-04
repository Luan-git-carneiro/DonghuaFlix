using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DonghuaFlix.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donghuas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Sinopse = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    Studio = table.Column<string>(type: "TEXT", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Genres = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Image = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donghuas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    DonghuaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 254, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, defaultValue: "Visistante"),
                    Status = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoAssets",
                columns: table => new
                {
                    EpisodeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataUpload = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CaminhoStorage = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoAssets", x => x.EpisodeId);
                    table.ForeignKey(
                        name: "FK_VideoAssets_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavorites",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DonghuaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateCreat = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavorites", x => new { x.UserId, x.DonghuaId });
                    table.ForeignKey(
                        name: "FK_UserFavorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserHistories",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EpisodeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateVisualization = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHistories", x => new { x.UserId, x.EpisodeId });
                    table.ForeignKey(
                        name: "FK_UserHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoManifests",
                columns: table => new
                {
                    VideoAssetId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CodecPrincipal = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Protocolo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoManifests", x => new { x.VideoAssetId, x.Id });
                    table.ForeignKey(
                        name: "FK_VideoManifests_VideoAssets_VideoAssetId",
                        column: x => x.VideoAssetId,
                        principalTable: "VideoAssets",
                        principalColumn: "EpisodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoQualityProfiles",
                columns: table => new
                {
                    VideoAssetId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ManifestId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quality = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Bitrate = table.Column<int>(type: "INTEGER", nullable: false),
                    Codec = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Path = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoQualityProfiles", x => new { x.VideoAssetId, x.ManifestId, x.Id });
                    table.ForeignKey(
                        name: "FK_VideoQualityProfiles_VideoManifests_VideoAssetId_ManifestId",
                        columns: x => new { x.VideoAssetId, x.ManifestId },
                        principalTable: "VideoManifests",
                        principalColumns: new[] { "VideoAssetId", "Id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Donghuas",
                columns: new[] { "Id", "CreatedAt", "Genres", "Image", "ReleaseDate", "Sinopse", "Status", "Studio", "Title", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1de89ff8-1124-4ea5-83c6-bdcaa1d1e355"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), 65568, "https://cdn.donghua.com/foghill.jpg", new DateTime(2020, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elementos e feras colidem em um mundo mitológico.", 3, "Samsara Studio", "Fog Hill of Five Elements", 3, null },
                    { new Guid("3bdb6119-fbe4-4e3e-a4bb-62d06b6871cc"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), 196608, "https://cdn.donghua.com/bigfish.jpg", new DateTime(2016, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "A alma de uma jovem navega entre o mundo humano e o espiritual.", 2, "B&T Studio", "Big Fish & Begonia", 2, null },
                    { new Guid("64d7f030-2fd4-4070-9753-1802fc4523ec"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), 16416, "https://cdn.donghua.com/kings-avatar.jpg", new DateTime(2017, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Um jogador profissional é forçado a sair do e-sport e começa de novo.", 2, "G.CMay Animation", "The King's Avatar", 1, null },
                    { new Guid("6b2c6cf0-d172-434f-878c-32f6a0842282"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), 32896, "https://cdn.donghua.com/immortalking.jpg", new DateTime(2020, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Um prodígio espiritual tenta levar uma vida normal.", 1, "Haoliners Animation", "The Daily Life of the Immortal King", 1, null },
                    { new Guid("a88bc6c5-f848-4fd5-a1e8-bbe7aa5a7e41"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), 18, "https://cdn.donghua.com/tgcf.jpg", new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Um deus exilado e um fantasma poderoso cruzam caminhos.", 1, "Haoliners Animation", "Heaven Official's Blessing", 1, null },
                    { new Guid("ab9d49a3-2e61-4c28-9aa9-2d0c91357df6"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), 65540, "https://cdn.donghua.com/greatlord.jpg", new DateTime(2021, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Um jovem órfão com poderes místicos luta para proteger sua irmã.", 1, "Big Firebird", "Spare Me, Great Lord!", 1, null },
                    { new Guid("c1270c8d-2e84-4f62-98ce-ef57c018e285"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), 96, "https://cdn.donghua.com/whitecat.jpg", new DateTime(2020, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Um oficial imperial felino investiga conspirações.", 1, "Nice Boat Animation", "White Cat Legend", 1, null },
                    { new Guid("d2a14412-b1e2-42c9-b7e0-4cd632df9dc8"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), 160, "https://cdn.donghua.com/scissor7.jpg", new DateTime(2018, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Um barbeiro com amnésia atua como assassino fracassado.", 1, "AHA Entertainment", "Scissor Seven", 1, null },
                    { new Guid("d5e6fb84-82f1-4950-bbee-2677d059d61c"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), 32800, "https://cdn.donghua.com/rakshasa.jpg", new DateTime(2016, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Espíritos protetores defendem os vivos dos mortos.", 2, "L2 Studio", "Rakshasa Street", 1, null },
                    { new Guid("f59fa054-16fc-4ad7-9ec7-26308bb9d774"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), 32800, "https://cdn.donghua.com/modaozushi.jpg", new DateTime(2018, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cultivadores espirituais enfrentam antigos segredos e traições.", 2, "Tencent Penguin Pictures", "Mo Dao Zu Shi", 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_DonghuaId",
                table: "Episodes",
                column: "DonghuaId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donghuas");

            migrationBuilder.DropTable(
                name: "UserFavorites");

            migrationBuilder.DropTable(
                name: "UserHistories");

            migrationBuilder.DropTable(
                name: "VideoQualityProfiles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VideoManifests");

            migrationBuilder.DropTable(
                name: "VideoAssets");

            migrationBuilder.DropTable(
                name: "Episodes");
        }
    }
}
