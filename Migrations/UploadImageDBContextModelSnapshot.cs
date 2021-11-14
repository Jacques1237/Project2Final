﻿// <auto-generated />
using Gallery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gallery.Migrations
{
    [DbContext(typeof(UploadImageDBContext))]
    partial class UploadImageDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<bool>("Featured")
                        .HasColumnType("bit");

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