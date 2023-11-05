﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tinder.Data;

#nullable disable

namespace Tinder.Migrations
{
    [DbContext(typeof(TinderContext))]
    partial class TinderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Tinder.Models.Discussion", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IdUser01")
                        .HasColumnType("int");

                    b.Property<int>("IdUser02")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("User01Id")
                        .HasColumnType("int");

                    b.Property<int?>("User02Id")
                        .HasColumnType("int");

                    b.Property<string>("dates")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("User01Id");

                    b.HasIndex("User02Id");

                    b.ToTable("Discussion");
                });

            modelBuilder.Entity("Tinder.Models.Locality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pays")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locality");
                });

            modelBuilder.Entity("Tinder.Models.MatchLike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdUser01")
                        .HasColumnType("int");

                    b.Property<int>("IdUser02")
                        .HasColumnType("int");

                    b.Property<string>("ScoreUser01")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScoreUser02")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("User01Id")
                        .HasColumnType("int");

                    b.Property<bool>("User01Like")
                        .HasColumnType("bit");

                    b.Property<int?>("User02Id")
                        .HasColumnType("int");

                    b.Property<bool>("User02Like")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("User01Id");

                    b.HasIndex("User02Id");

                    b.ToTable("MatchLike");
                });

            modelBuilder.Entity("Tinder.Models.Questions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reponse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Tinder.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hobbys")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocalityId")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhotoJson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LocalityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tinder.Models.Discussion", b =>
                {
                    b.HasOne("Tinder.Models.Users", "User01")
                        .WithMany("Discussion01")
                        .HasForeignKey("User01Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Tinder.Models.Users", "User02")
                        .WithMany("Discussion02")
                        .HasForeignKey("User02Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("User01");

                    b.Navigation("User02");
                });

            modelBuilder.Entity("Tinder.Models.MatchLike", b =>
                {
                    b.HasOne("Tinder.Models.Users", "User01")
                        .WithMany("MatchLike01")
                        .HasForeignKey("User01Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Tinder.Models.Users", "User02")
                        .WithMany("MatchLike02")
                        .HasForeignKey("User02Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("User01");

                    b.Navigation("User02");
                });

            modelBuilder.Entity("Tinder.Models.Questions", b =>
                {
                    b.HasOne("Tinder.Models.Users", "User")
                        .WithMany("Questions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tinder.Models.Users", b =>
                {
                    b.HasOne("Tinder.Models.Locality", "Locality")
                        .WithMany("Users")
                        .HasForeignKey("LocalityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Locality");
                });

            modelBuilder.Entity("Tinder.Models.Locality", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Tinder.Models.Users", b =>
                {
                    b.Navigation("Discussion01");

                    b.Navigation("Discussion02");

                    b.Navigation("MatchLike01");

                    b.Navigation("MatchLike02");

                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
