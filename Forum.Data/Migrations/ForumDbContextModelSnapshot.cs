﻿// <auto-generated />
using System;
using Forum.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Forum.Data.Migrations
{
    [DbContext(typeof(ForumDbContext))]
    partial class ForumDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Forum.Domain.Models.Account.Permission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Forum.Domain.Models.Account.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Ban")
                        .HasColumnType("bit");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Display_Name")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Mobile")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Forum.Domain.Models.Account.UserPermission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("Permission_id")
                        .HasColumnType("bigint");

                    b.Property<long>("User_Id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Permission_id");

                    b.HasIndex("User_Id");

                    b.ToTable("UserPermissions");
                });

            modelBuilder.Entity("Forum.Domain.Models.Content.Banner", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Banners");
                });

            modelBuilder.Entity("Forum.Domain.Models.Questions.Answer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorrectAnswer")
                        .HasColumnType("bit");

                    b.Property<long>("Question_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Updated_at")
                        .HasColumnType("datetime2");

                    b.Property<long>("User_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Question_id");

                    b.HasIndex("User_id");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Forum.Domain.Models.Questions.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("Category_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime?>("Updated_at")
                        .HasColumnType("datetime2");

                    b.Property<long>("User_id")
                        .HasColumnType("bigint");

                    b.Property<int>("Visit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Category_id");

                    b.HasIndex("User_id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Forum.Domain.Models.Questions.QuestionCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("QuestionCategories");
                });

            modelBuilder.Entity("Forum.Domain.Models.Questions.QuestionTag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted_at")
                        .HasColumnType("datetime2");

                    b.Property<long>("Question_id")
                        .HasColumnType("bigint");

                    b.Property<long>("Tag_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Updated_at")
                        .HasColumnType("datetime2");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Question_id");

                    b.HasIndex("Tag_id");

                    b.HasIndex("UserId");

                    b.ToTable("QuestionTags");
                });

            modelBuilder.Entity("Forum.Domain.Models.Questions.QuestionVisit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted_at")
                        .HasColumnType("datetime2");

                    b.Property<long>("Question_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Updated_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("User_Ip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Question_id");

                    b.ToTable("QuestionVisits");
                });

            modelBuilder.Entity("Forum.Domain.Models.Tags.RequestTag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated_at")
                        .HasColumnType("datetime2");

                    b.Property<long>("User_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("User_id");

                    b.ToTable("RequestTags");
                });

            modelBuilder.Entity("Forum.Domain.Models.Tags.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Forum.Domain.Models.Account.UserPermission", b =>
                {
                    b.HasOne("Forum.Domain.Models.Account.Permission", "Permission")
                        .WithMany("UserPermissions")
                        .HasForeignKey("Permission_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Forum.Domain.Models.Account.User", "User")
                        .WithMany("UserPermissions")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Forum.Domain.Models.Questions.Answer", b =>
                {
                    b.HasOne("Forum.Domain.Models.Questions.Question", "Question")
                        .WithMany()
                        .HasForeignKey("Question_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Forum.Domain.Models.Account.User", "User")
                        .WithMany("Answers")
                        .HasForeignKey("User_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Forum.Domain.Models.Questions.Question", b =>
                {
                    b.HasOne("Forum.Domain.Models.Questions.QuestionCategory", "QuestionCategory")
                        .WithMany()
                        .HasForeignKey("Category_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Forum.Domain.Models.Account.User", "User")
                        .WithMany("Questions")
                        .HasForeignKey("User_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("QuestionCategory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Forum.Domain.Models.Questions.QuestionTag", b =>
                {
                    b.HasOne("Forum.Domain.Models.Questions.Question", "Question")
                        .WithMany()
                        .HasForeignKey("Question_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Forum.Domain.Models.Tags.Tag", "Tag")
                        .WithMany("QuestionTags")
                        .HasForeignKey("Tag_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Forum.Domain.Models.Account.User", null)
                        .WithMany("QuestionTags")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Question");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Forum.Domain.Models.Questions.QuestionVisit", b =>
                {
                    b.HasOne("Forum.Domain.Models.Questions.Question", "Question")
                        .WithMany("QuestionVisits")
                        .HasForeignKey("Question_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Forum.Domain.Models.Tags.RequestTag", b =>
                {
                    b.HasOne("Forum.Domain.Models.Account.User", "User")
                        .WithMany("RequestTags")
                        .HasForeignKey("User_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Forum.Domain.Models.Account.Permission", b =>
                {
                    b.Navigation("UserPermissions");
                });

            modelBuilder.Entity("Forum.Domain.Models.Account.User", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("QuestionTags");

                    b.Navigation("Questions");

                    b.Navigation("RequestTags");

                    b.Navigation("UserPermissions");
                });

            modelBuilder.Entity("Forum.Domain.Models.Questions.Question", b =>
                {
                    b.Navigation("QuestionVisits");
                });

            modelBuilder.Entity("Forum.Domain.Models.Tags.Tag", b =>
                {
                    b.Navigation("QuestionTags");
                });
#pragma warning restore 612, 618
        }
    }
}