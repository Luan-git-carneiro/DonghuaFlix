﻿// <auto-generated />
using System;
using DonghuaFlix.Backend.src.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DonghuaFlix.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250522145309_SeedDonghuasII")]
    partial class SeedDonghuasII
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.5");

            modelBuilder.Entity("DonghuaFlix.Backend.src.Core.Domain.Entities.Donghua", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("Genres")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Image")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sinopse")
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Studio")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Donghuas", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("64d7f030-2fd4-4070-9753-1802fc4523ec"),
                            CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                            Genres = 16416,
                            Image = "https://cdn.donghua.com/kings-avatar.jpg",
                            ReleaseDate = new DateTime(2017, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Sinopse = "Um jogador profissional é forçado a sair do e-sport e começa de novo.",
                            Status = 2,
                            Studio = "G.CMay Animation",
                            Title = "The King's Avatar",
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("f59fa054-16fc-4ad7-9ec7-26308bb9d774"),
                            CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                            Genres = 32800,
                            Image = "https://cdn.donghua.com/modaozushi.jpg",
                            ReleaseDate = new DateTime(2018, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Sinopse = "Cultivadores espirituais enfrentam antigos segredos e traições.",
                            Status = 2,
                            Studio = "Tencent Penguin Pictures",
                            Title = "Mo Dao Zu Shi",
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("a88bc6c5-f848-4fd5-a1e8-bbe7aa5a7e41"),
                            CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                            Genres = 18,
                            Image = "https://cdn.donghua.com/tgcf.jpg",
                            ReleaseDate = new DateTime(2020, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Sinopse = "Um deus exilado e um fantasma poderoso cruzam caminhos.",
                            Status = 1,
                            Studio = "Haoliners Animation",
                            Title = "Heaven Official's Blessing",
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("d2a14412-b1e2-42c9-b7e0-4cd632df9dc8"),
                            CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                            Genres = 160,
                            Image = "https://cdn.donghua.com/scissor7.jpg",
                            ReleaseDate = new DateTime(2018, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Sinopse = "Um barbeiro com amnésia atua como assassino fracassado.",
                            Status = 1,
                            Studio = "AHA Entertainment",
                            Title = "Scissor Seven",
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("c1270c8d-2e84-4f62-98ce-ef57c018e285"),
                            CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                            Genres = 96,
                            Image = "https://cdn.donghua.com/whitecat.jpg",
                            ReleaseDate = new DateTime(2020, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Sinopse = "Um oficial imperial felino investiga conspirações.",
                            Status = 1,
                            Studio = "Nice Boat Animation",
                            Title = "White Cat Legend",
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("1de89ff8-1124-4ea5-83c6-bdcaa1d1e355"),
                            CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                            Genres = 65568,
                            Image = "https://cdn.donghua.com/foghill.jpg",
                            ReleaseDate = new DateTime(2020, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Sinopse = "Elementos e feras colidem em um mundo mitológico.",
                            Status = 3,
                            Studio = "Samsara Studio",
                            Title = "Fog Hill of Five Elements",
                            Type = 3
                        },
                        new
                        {
                            Id = new Guid("6b2c6cf0-d172-434f-878c-32f6a0842282"),
                            CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                            Genres = 32896,
                            Image = "https://cdn.donghua.com/immortalking.jpg",
                            ReleaseDate = new DateTime(2020, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Sinopse = "Um prodígio espiritual tenta levar uma vida normal.",
                            Status = 1,
                            Studio = "Haoliners Animation",
                            Title = "The Daily Life of the Immortal King",
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("3bdb6119-fbe4-4e3e-a4bb-62d06b6871cc"),
                            CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                            Genres = 196608,
                            Image = "https://cdn.donghua.com/bigfish.jpg",
                            ReleaseDate = new DateTime(2016, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Sinopse = "A alma de uma jovem navega entre o mundo humano e o espiritual.",
                            Status = 2,
                            Studio = "B&T Studio",
                            Title = "Big Fish & Begonia",
                            Type = 2
                        },
                        new
                        {
                            Id = new Guid("d5e6fb84-82f1-4950-bbee-2677d059d61c"),
                            CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                            Genres = 32800,
                            Image = "https://cdn.donghua.com/rakshasa.jpg",
                            ReleaseDate = new DateTime(2016, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Sinopse = "Espíritos protetores defendem os vivos dos mortos.",
                            Status = 2,
                            Studio = "L2 Studio",
                            Title = "Rakshasa Street",
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("ab9d49a3-2e61-4c28-9aa9-2d0c91357df6"),
                            CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                            Genres = 65540,
                            Image = "https://cdn.donghua.com/greatlord.jpg",
                            ReleaseDate = new DateTime(2021, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Sinopse = "Um jovem órfão com poderes místicos luta para proteger sua irmã.",
                            Status = 1,
                            Studio = "Big Firebird",
                            Title = "Spare Me, Great Lord!",
                            Type = 1
                        });
                });

            modelBuilder.Entity("DonghuaFlix.Backend.src.Core.Domain.Entities.Episode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DonghuaId")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DonghuaId");

                    b.ToTable("Episodes", (string)null);
                });

            modelBuilder.Entity("DonghuaFlix.Backend.src.Core.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasDefaultValue("Visistante");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("DonghuaFlix.Backend.src.Core.Domain.Entities.Episode", b =>
                {
                    b.OwnsOne("DonghuaFlix.Backend.src.Core.Domain.Entities.VideoAsset", "Video", b1 =>
                        {
                            b1.Property<Guid>("EpisodeId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("CaminhoStorage")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("DataUpload")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("Id")
                                .HasColumnType("TEXT");

                            b1.Property<DateTime?>("UpdatedAt")
                                .HasColumnType("TEXT");

                            b1.HasKey("EpisodeId");

                            b1.ToTable("VideoAssets", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("EpisodeId");

                            b1.OwnsMany("DonghuaFlix.Backend.src.Core.Domain.ValueObjects.VideoManifest", "Manifests", b2 =>
                                {
                                    b2.Property<Guid>("VideoAssetId")
                                        .HasColumnType("TEXT");

                                    b2.Property<Guid>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("CodecPrincipal")
                                        .IsRequired()
                                        .HasMaxLength(10)
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("Protocolo")
                                        .IsRequired()
                                        .HasMaxLength(10)
                                        .HasColumnType("TEXT");

                                    b2.HasKey("VideoAssetId", "Id");

                                    b2.ToTable("VideoManifests", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("VideoAssetId");

                                    b2.OwnsMany("DonghuaFlix.Backend.src.Core.Domain.ValueObjects.VideoQualityProfile", "Qualities", b3 =>
                                        {
                                            b3.Property<Guid>("VideoAssetId")
                                                .HasColumnType("TEXT");

                                            b3.Property<Guid>("ManifestId")
                                                .HasColumnType("TEXT");

                                            b3.Property<Guid>("Id")
                                                .ValueGeneratedOnAdd()
                                                .HasColumnType("TEXT");

                                            b3.Property<int>("Bitrate")
                                                .HasColumnType("INTEGER");

                                            b3.Property<string>("Codec")
                                                .IsRequired()
                                                .HasMaxLength(20)
                                                .HasColumnType("TEXT");

                                            b3.Property<string>("Path")
                                                .IsRequired()
                                                .HasMaxLength(500)
                                                .HasColumnType("TEXT");

                                            b3.Property<string>("Quality")
                                                .IsRequired()
                                                .HasMaxLength(20)
                                                .HasColumnType("TEXT");

                                            b3.HasKey("VideoAssetId", "ManifestId", "Id");

                                            b3.ToTable("VideoQualityProfiles", (string)null);

                                            b3.WithOwner()
                                                .HasForeignKey("VideoAssetId", "ManifestId");
                                        });

                                    b2.Navigation("Qualities");
                                });

                            b1.Navigation("Manifests");
                        });

                    b.Navigation("Video")
                        .IsRequired();
                });

            modelBuilder.Entity("DonghuaFlix.Backend.src.Core.Domain.Entities.User", b =>
                {
                    b.OwnsOne("DonghuaFlix.Backend.src.Core.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Valor")
                                .IsRequired()
                                .HasMaxLength(254)
                                .HasColumnType("TEXT")
                                .HasColumnName("Email");

                            b1.HasKey("UserId");

                            b1.HasIndex("Valor")
                                .IsUnique();

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsMany("DonghuaFlix.Backend.src.Core.Domain.ValueObjects.Favorite", "Favorites", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("DonghuaId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("DateCreat")
                                .HasColumnType("TEXT");

                            b1.HasKey("UserId", "DonghuaId");

                            b1.ToTable("UserFavorites", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsMany("DonghuaFlix.Backend.src.Core.Domain.ValueObjects.History", "Histories", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("EpisodeId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("DateVisualization")
                                .HasColumnType("TEXT");

                            b1.HasKey("UserId", "EpisodeId");

                            b1.ToTable("UserHistories", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("DonghuaFlix.Backend.src.Core.Domain.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(60)
                                .HasColumnType("TEXT")
                                .HasColumnName("PasswordHash");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Favorites");

                    b.Navigation("Histories");

                    b.Navigation("Password")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
