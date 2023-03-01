﻿// <auto-generated />
using System;
using MicroserviceMail.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MicroserviceMail.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MicroserviceMail.Domain.Mail", b =>
                {
                    b.Property<int>("MailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MailId"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("Varchar(2000)");

                    b.Property<string>("Cc")
                        .IsRequired()
                        .HasColumnType("Varchar(2000)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("Varchar(200)");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("Varchar(200)");

                    b.Property<bool>("Sent")
                        .HasColumnType("boolean");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("Varchar(1000)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("Varchar(200)");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("Varchar(2000)");

                    b.HasKey("MailId");

                    b.ToTable("Mail", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
