﻿// <auto-generated />
using Gallery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gallery.Migrations
{
    [DbContext(typeof(UploadImageDBContext))]
    [Migration("20210108115316_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Gallery.Models.Images", b =>
                {
                    b.Property<int>("ImagesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ImagesDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagesPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImagesId");

                    b.ToTable("Image");
                });
#pragma warning restore 612, 618
        }
    }
}
