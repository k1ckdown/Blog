﻿// <auto-generated />
using System;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Comment", b =>
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

            modelBuilder.Entity("Domain.Entities.Community", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Communities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("84367e0d-5b35-4ae1-81ef-ce2ba6974f19"),
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 379, DateTimeKind.Utc).AddTicks(9300),
                            Description = "Сообщество разработчиков, где можно делиться опытом, обсуждать новейшие технологии и находить интересные проекты.",
                            IsClosed = true,
                            Name = "IT Geeks"
                        },
                        new
                        {
                            Id = new Guid("92d6b5bb-4977-4507-a281-9872a2f93590"),
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 379, DateTimeKind.Utc).AddTicks(9300),
                            Description = "Сообщество спортивных энтузиастов, где можно обсуждать тренировки, соревнования и делиться своими достижениями в спорте.",
                            IsClosed = false,
                            Name = "Sport Enthusiasts"
                        },
                        new
                        {
                            Id = new Guid("01a705ae-7f35-46a5-b8d6-e07be527893b"),
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 379, DateTimeKind.Utc).AddTicks(9300),
                            Description = "Сообщество любителей английского языка, где можно улучшить свои навыки, общаясь с носителями языка и принимая участие в языковых встречах.",
                            IsClosed = false,
                            Name = "English Lovers"
                        },
                        new
                        {
                            Id = new Guid("a7aba6b2-31ce-45d4-be78-17ff89a3b04a"),
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 379, DateTimeKind.Utc).AddTicks(9300),
                            Description = "Сообщество автолюбителей, где можно обсуждать последние новости в автомобильной индустрии, делииться опытом по тюнингу авто и проводить встречи на автошоу.",
                            IsClosed = true,
                            Name = "Auto Enthusiasts"
                        },
                        new
                        {
                            Id = new Guid("53817554-2518-406d-b1f6-4b1f2e4cedc3"),
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 379, DateTimeKind.Utc).AddTicks(9300),
                            Description = "Сообщество стартапов в сфере информационных технологий, где можно найти соучредителей, получить обратную связь на идеи и найти инвесторов для своего проекта.",
                            IsClosed = true,
                            Name = "Tech Startups"
                        });
                });

            modelBuilder.Entity("Domain.Entities.CommunityAdmin", b =>
                {
                    b.Property<Guid>("CommunityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("CommunityId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CommunityAdmins");

                    b.HasData(
                        new
                        {
                            CommunityId = new Guid("53817554-2518-406d-b1f6-4b1f2e4cedc3"),
                            UserId = new Guid("b2a55a66-33fd-471b-83ae-094dd6a3cda3")
                        },
                        new
                        {
                            CommunityId = new Guid("53817554-2518-406d-b1f6-4b1f2e4cedc3"),
                            UserId = new Guid("51538053-0c9f-4139-a17c-996b09935c85")
                        },
                        new
                        {
                            CommunityId = new Guid("84367e0d-5b35-4ae1-81ef-ce2ba6974f19"),
                            UserId = new Guid("f200805b-dc0a-4340-8351-c92bf9a8d37c")
                        },
                        new
                        {
                            CommunityId = new Guid("84367e0d-5b35-4ae1-81ef-ce2ba6974f19"),
                            UserId = new Guid("b2a55a66-33fd-471b-83ae-094dd6a3cda3")
                        },
                        new
                        {
                            CommunityId = new Guid("92d6b5bb-4977-4507-a281-9872a2f93590"),
                            UserId = new Guid("cef7e70a-ce99-48d9-81a1-e18b1d34a7d6")
                        },
                        new
                        {
                            CommunityId = new Guid("a7aba6b2-31ce-45d4-be78-17ff89a3b04a"),
                            UserId = new Guid("67473056-077d-44e8-bbbf-f273072cce83")
                        },
                        new
                        {
                            CommunityId = new Guid("01a705ae-7f35-46a5-b8d6-e07be527893b"),
                            UserId = new Guid("52a94c73-6958-402c-8d1e-abe16e81cc22")
                        });
                });

            modelBuilder.Entity("Domain.Entities.Like", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Domain.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CommunityId")
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

                    b.HasIndex("CommunityId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Domain.Entities.Subscription", b =>
                {
                    b.Property<Guid>("CommunityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("CommunityId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Domain.Entities.Tag", b =>
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
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810),
                            Name = "it"
                        },
                        new
                        {
                            Id = new Guid("3f34aaa1-b6be-432d-9ffc-aee460e3b7d7"),
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810),
                            Name = "18+"
                        },
                        new
                        {
                            Id = new Guid("cada0a21-a535-4126-ae94-b3f0f1171415"),
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810),
                            Name = "соцсети"
                        },
                        new
                        {
                            Id = new Guid("542a25a1-9b47-4b43-a1b3-8e98018fd5ab"),
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810),
                            Name = "интернет"
                        },
                        new
                        {
                            Id = new Guid("f8bf2f1b-48bb-4749-b984-0c9856093ba0"),
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810),
                            Name = "история"
                        },
                        new
                        {
                            Id = new Guid("3070c757-f147-4576-947e-3c0d89494fcb"),
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810),
                            Name = "приколы"
                        },
                        new
                        {
                            Id = new Guid("0f7740e0-8fed-4721-bc0e-7d2eac48434e"),
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810),
                            Name = "косплей"
                        },
                        new
                        {
                            Id = new Guid("4a239805-e276-455e-b0a1-ad3b2958ac70"),
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810),
                            Name = "преступление"
                        },
                        new
                        {
                            Id = new Guid("3fc1a0fb-b836-4eb2-83b8-9d56eb8d93d5"),
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810),
                            Name = "еда"
                        },
                        new
                        {
                            Id = new Guid("a80d3dd8-b6c1-4d85-959f-1433ab1523b5"),
                            CreateTime = new DateTime(2023, 12, 3, 14, 51, 33, 377, DateTimeKind.Utc).AddTicks(3810),
                            Name = "теория заговора"
                        });
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
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

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.HasOne("Domain.Entities.Comment", null)
                        .WithMany("SubComments")
                        .HasForeignKey("CommentId");

                    b.HasOne("Domain.Entities.Post", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostId");

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.CommunityAdmin", b =>
                {
                    b.HasOne("Domain.Entities.Community", null)
                        .WithMany()
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Like", b =>
                {
                    b.HasOne("Domain.Entities.Post", null)
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Post", b =>
                {
                    b.HasOne("Domain.Entities.Community", "Community")
                        .WithMany()
                        .HasForeignKey("CommunityId");

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Subscription", b =>
                {
                    b.HasOne("Domain.Entities.Community", null)
                        .WithMany()
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("Domain.Entities.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.Navigation("SubComments");
                });

            modelBuilder.Entity("Domain.Entities.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
