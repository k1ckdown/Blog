﻿// <auto-generated />
using System;
using Blog.Persistence.Contexts;
using Blog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Blog.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231130153246_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Blog.Domain.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CommentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Blog.Domain.Entities.Like", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Blog.Domain.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<int>("ReadingTime")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Blog.Domain.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cbbee647-d1db-4b9b-b053-f9f640bb97d8"),
                            CreateTime = new DateTime(2023, 11, 30, 15, 32, 46, 297, DateTimeKind.Utc).AddTicks(9990),
                            Name = "it"
                        },
                        new
                        {
                            Id = new Guid("3f34aaa1-b6be-432d-9ffc-aee460e3b7d7"),
                            CreateTime = new DateTime(2023, 11, 30, 15, 32, 46, 297, DateTimeKind.Utc).AddTicks(9990),
                            Name = "18+"
                        },
                        new
                        {
                            Id = new Guid("cada0a21-a535-4126-ae94-b3f0f1171415"),
                            CreateTime = new DateTime(2023, 11, 30, 15, 32, 46, 297, DateTimeKind.Utc).AddTicks(9990),
                            Name = "соцсети"
                        },
                        new
                        {
                            Id = new Guid("542a25a1-9b47-4b43-a1b3-8e98018fd5ab"),
                            CreateTime = new DateTime(2023, 11, 30, 15, 32, 46, 297, DateTimeKind.Utc).AddTicks(9990),
                            Name = "интернет"
                        },
                        new
                        {
                            Id = new Guid("f8bf2f1b-48bb-4749-b984-0c9856093ba0"),
                            CreateTime = new DateTime(2023, 11, 30, 15, 32, 46, 298, DateTimeKind.Utc),
                            Name = "история"
                        },
                        new
                        {
                            Id = new Guid("3070c757-f147-4576-947e-3c0d89494fcb"),
                            CreateTime = new DateTime(2023, 11, 30, 15, 32, 46, 298, DateTimeKind.Utc),
                            Name = "приколы"
                        },
                        new
                        {
                            Id = new Guid("0f7740e0-8fed-4721-bc0e-7d2eac48434e"),
                            CreateTime = new DateTime(2023, 11, 30, 15, 32, 46, 298, DateTimeKind.Utc),
                            Name = "косплей"
                        },
                        new
                        {
                            Id = new Guid("4a239805-e276-455e-b0a1-ad3b2958ac70"),
                            CreateTime = new DateTime(2023, 11, 30, 15, 32, 46, 298, DateTimeKind.Utc),
                            Name = "преступление"
                        },
                        new
                        {
                            Id = new Guid("3fc1a0fb-b836-4eb2-83b8-9d56eb8d93d5"),
                            CreateTime = new DateTime(2023, 11, 30, 15, 32, 46, 298, DateTimeKind.Utc),
                            Name = "еда"
                        },
                        new
                        {
                            Id = new Guid("a80d3dd8-b6c1-4d85-959f-1433ab1523b5"),
                            CreateTime = new DateTime(2023, 11, 30, 15, 32, 46, 298, DateTimeKind.Utc),
                            Name = "теория заговора"
                        });
                });

            modelBuilder.Entity("Blog.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<Guid>("PostsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uuid");

                    b.HasKey("PostsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("Blog.Domain.Entities.Comment", b =>
                {
                    b.HasOne("Blog.Domain.Entities.Comment", null)
                        .WithMany("SubComments")
                        .HasForeignKey("CommentId");

                    b.HasOne("Blog.Domain.Entities.Post", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostId");

                    b.HasOne("Blog.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Blog.Domain.Entities.Like", b =>
                {
                    b.HasOne("Blog.Domain.Entities.Post", null)
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blog.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Blog.Domain.Entities.Post", b =>
                {
                    b.HasOne("Blog.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("Blog.Domain.Entities.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blog.Domain.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Blog.Domain.Entities.Comment", b =>
                {
                    b.Navigation("SubComments");
                });

            modelBuilder.Entity("Blog.Domain.Entities.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}