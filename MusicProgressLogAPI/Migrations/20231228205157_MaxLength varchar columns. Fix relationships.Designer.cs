﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicProgressLogAPI.Data;

#nullable disable

namespace MusicProgressLogAPI.Migrations
{
    [DbContext(typeof(MusicProgressLogDbContext))]
    [Migration("20231228205157_MaxLength varchar columns. Fix relationships")]
    partial class MaxLengthvarcharcolumnsFixrelationships
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MusicProgressLogAPI.Models.Domain.AudioFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("FileData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileLocation")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MIMEType")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("ProgressLogId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProgressLogId")
                        .IsUnique();

                    b.ToTable("AudioFiles");
                });

            modelBuilder.Entity("MusicProgressLogAPI.Models.Domain.Piece", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Composer")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Instrument")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("UserRelationshipId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserRelationshipId");

                    b.ToTable("Pieces");
                });

            modelBuilder.Entity("MusicProgressLogAPI.Models.Domain.ProgressLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("PieceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("UserRelationshipId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PieceId");

                    b.HasIndex("UserRelationshipId");

                    b.ToTable("ProgressLogs");
                });

            modelBuilder.Entity("MusicProgressLogAPI.Models.Domain.UserRelationship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRelationships");
                });

            modelBuilder.Entity("MusicProgressLogAPI.Models.Domain.AudioFile", b =>
                {
                    b.HasOne("MusicProgressLogAPI.Models.Domain.ProgressLog", null)
                        .WithOne("AudioFile")
                        .HasForeignKey("MusicProgressLogAPI.Models.Domain.AudioFile", "ProgressLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MusicProgressLogAPI.Models.Domain.Piece", b =>
                {
                    b.HasOne("MusicProgressLogAPI.Models.Domain.UserRelationship", null)
                        .WithMany("Pieces")
                        .HasForeignKey("UserRelationshipId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("MusicProgressLogAPI.Models.Domain.ProgressLog", b =>
                {
                    b.HasOne("MusicProgressLogAPI.Models.Domain.Piece", "Piece")
                        .WithMany()
                        .HasForeignKey("PieceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MusicProgressLogAPI.Models.Domain.UserRelationship", null)
                        .WithMany("ProgressLogs")
                        .HasForeignKey("UserRelationshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Piece");
                });

            modelBuilder.Entity("MusicProgressLogAPI.Models.Domain.ProgressLog", b =>
                {
                    b.Navigation("AudioFile");
                });

            modelBuilder.Entity("MusicProgressLogAPI.Models.Domain.UserRelationship", b =>
                {
                    b.Navigation("Pieces");

                    b.Navigation("ProgressLogs");
                });
#pragma warning restore 612, 618
        }
    }
}