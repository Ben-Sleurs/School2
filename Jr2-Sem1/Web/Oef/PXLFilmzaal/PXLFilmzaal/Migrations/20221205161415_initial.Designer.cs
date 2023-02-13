﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PXLFilmzaal.Data;

#nullable disable

namespace PXLFilmzaal.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221205161415_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PXLFilmzaal.Models.Data.Film", b =>
                {
                    b.Property<string>("FilmId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("FilmImageId")
                        .HasColumnType("int");

                    b.Property<string>("FilmImageId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FilmNaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FilmId");

                    b.HasIndex("FilmImageId1");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("PXLFilmzaal.Models.Data.FilmImage", b =>
                {
                    b.Property<string>("FilmImageId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("FilmImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FilmImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FilmImageId");

                    b.ToTable("FilmImages");
                });

            modelBuilder.Entity("PXLFilmzaal.Models.Data.Film", b =>
                {
                    b.HasOne("PXLFilmzaal.Models.Data.FilmImage", "FilmImage")
                        .WithMany("Films")
                        .HasForeignKey("FilmImageId1");

                    b.Navigation("FilmImage");
                });

            modelBuilder.Entity("PXLFilmzaal.Models.Data.FilmImage", b =>
                {
                    b.Navigation("Films");
                });
#pragma warning restore 612, 618
        }
    }
}
