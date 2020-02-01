﻿// <auto-generated />
using System;
using MapTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MapTest.Migrations
{
    [DbContext(typeof(EFCoreTestContext))]
    partial class MvcMovieContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MapTest.Models.Article", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("MapTest.Models.ArticleTag", b =>
                {
                    b.Property<Guid>("TagID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArticleID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TagID", "ArticleID");

                    b.HasIndex("ArticleID");

                    b.ToTable("ArticleTags");
                });

            modelBuilder.Entity("MapTest.Models.FederalDistrict", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("FederalDistricts");
                });

            modelBuilder.Entity("MapTest.Models.FederalSubject", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FederalDistrictID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("FederalDistrictID");

                    b.ToTable("FederalSubjects");
                });

            modelBuilder.Entity("MapTest.Models.Tag", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("ID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("MapTest.Models.ArticleTag", b =>
                {
                    b.HasOne("MapTest.Models.Article", "Article")
                        .WithMany("ArticleTags")
                        .HasForeignKey("ArticleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MapTest.Models.Tag", "Tag")
                        .WithMany("ArticleTags")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MapTest.Models.FederalSubject", b =>
                {
                    b.HasOne("MapTest.Models.FederalDistrict", "FederalDistrict")
                        .WithMany("FederalSubjects")
                        .HasForeignKey("FederalDistrictID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
