﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechnicalRadiation.Repositories.Contexts;

namespace TechnicalRadiation.WebApi.Migrations
{
    [DbContext(typeof(NewsDbContext))]
    partial class NewsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.AuthorNewsItem", b =>
                {
                    b.Property<int>("AuthorsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NewsItemsId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("NewsItemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AuthorsId", "NewsItemsId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("NewsItemId");

                    b.ToTable("AuthorNewsItem");
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

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.CategoryNewsItem", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NewsItemsId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("NewsItemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CategoriesId", "NewsItemsId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("NewsItemId");

                    b.ToTable("CategoryNewsItem");
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

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.AuthorNewsItem", b =>
                {
                    b.HasOne("TechnicalRadiation.Models.Entities.Author", "Author")
                        .WithMany("NewsItemLink")
                        .HasForeignKey("AuthorId");

                    b.HasOne("TechnicalRadiation.Models.Entities.NewsItem", "NewsItem")
                        .WithMany("AuthorNewsItemLink")
                        .HasForeignKey("NewsItemId");

                    b.Navigation("Author");

                    b.Navigation("NewsItem");
                });

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.CategoryNewsItem", b =>
                {
                    b.HasOne("TechnicalRadiation.Models.Entities.Category", "Category")
                        .WithMany("NewsItemLink")
                        .HasForeignKey("CategoryId");

                    b.HasOne("TechnicalRadiation.Models.Entities.NewsItem", "NewsItem")
                        .WithMany("CategoryNewsItemLink")
                        .HasForeignKey("NewsItemId");

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
                    b.Navigation("AuthorNewsItemLink");

                    b.Navigation("CategoryNewsItemLink");
                });
#pragma warning restore 612, 618
        }
    }
}
