﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechnicalRadiation.Repositories.Contexts;

namespace TechnicalRadiation.WebApi.Migrations
{
    [DbContext(typeof(NewsDbContext))]
    [Migration("20210917135239_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfileImgSource")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.NewsItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImgSource")
                        .HasColumnType("TEXT");

                    b.Property<string>("LongDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("NewsItems");
                });

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.NewsItemAuthors", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NewsItemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AuthorId", "NewsItemId");

                    b.HasIndex("NewsItemId");

                    b.ToTable("NewsItemAuthors");
                });

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.NewsItemCategories", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NewsItemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CategoryId", "NewsItemId");

                    b.HasIndex("NewsItemId");

                    b.ToTable("NewsItemCategories");
                });

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.NewsItemAuthors", b =>
                {
                    b.HasOne("TechnicalRadiation.Models.Entities.Author", "Author")
                        .WithMany("NewsItemLink")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechnicalRadiation.Models.Entities.NewsItem", "NewsItem")
                        .WithMany("NewsItemAuthorLink")
                        .HasForeignKey("NewsItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("NewsItem");
                });

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.NewsItemCategories", b =>
                {
                    b.HasOne("TechnicalRadiation.Models.Entities.Category", "Category")
                        .WithMany("NewsItemLink")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechnicalRadiation.Models.Entities.NewsItem", "NewsItem")
                        .WithMany("NewsItemCategoryLink")
                        .HasForeignKey("NewsItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("NewsItem");
                });

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.Author", b =>
                {
                    b.Navigation("NewsItemLink");
                });

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.Category", b =>
                {
                    b.Navigation("NewsItemLink");
                });

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.NewsItem", b =>
                {
                    b.Navigation("NewsItemAuthorLink");

                    b.Navigation("NewsItemCategoryLink");
                });
#pragma warning restore 612, 618
        }
    }
}
