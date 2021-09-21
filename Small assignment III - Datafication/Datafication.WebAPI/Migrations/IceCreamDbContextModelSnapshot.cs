﻿// <auto-generated />
using System;
using Datafication.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Datafication.WebAPI.Migrations
{
    [DbContext(typeof(IceCreamDbContext))]
    partial class IceCreamDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("CategoryIceCream", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IceCreamsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CategoriesId", "IceCreamsId");

                    b.HasIndex("IceCreamsId");

                    b.ToTable("CategoryIceCream");
                });

            modelBuilder.Entity("Datafication.Repositories.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Datafication.Repositories.Entities.IceCream", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("IceCreams");
                });

            modelBuilder.Entity("Datafication.Repositories.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IceCreamId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IceCreamId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Datafication.Repositories.Entities.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CategoryOccurranceId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExternalUrl")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryOccurranceId");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("CategoryIceCream", b =>
                {
                    b.HasOne("Datafication.Repositories.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Datafication.Repositories.Entities.IceCream", null)
                        .WithMany()
                        .HasForeignKey("IceCreamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Datafication.Repositories.Entities.IceCream", b =>
                {
                    b.HasOne("Datafication.Repositories.Entities.Manufacturer", "Manufacturer")
                        .WithMany("IceCreams")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("Datafication.Repositories.Entities.Image", b =>
                {
                    b.HasOne("Datafication.Repositories.Entities.IceCream", "IceCream")
                        .WithMany("Images")
                        .HasForeignKey("IceCreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IceCream");
                });

            modelBuilder.Entity("Datafication.Repositories.Entities.Manufacturer", b =>
                {
                    b.HasOne("Datafication.Repositories.Entities.Category", "CategoryOccurrance")
                        .WithMany()
                        .HasForeignKey("CategoryOccurranceId");

                    b.Navigation("CategoryOccurrance");
                });

            modelBuilder.Entity("Datafication.Repositories.Entities.IceCream", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Datafication.Repositories.Entities.Manufacturer", b =>
                {
                    b.Navigation("IceCreams");
                });
#pragma warning restore 612, 618
        }
    }
}
