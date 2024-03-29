﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notex.Infrastructure.EventSourcing;

#nullable disable

namespace Notex.Infrastructure.EventSourcing.Migrations
{
    [DbContext(typeof(EventSourcingDbContext))]
    partial class EventSourcingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Notex.Infrastructure.EventSourcing.EventEntity", b =>
                {
                    b.Property<Guid>("SourcedId")
                        .HasColumnType("char(36)")
                        .HasColumnName("sourced_id");

                    b.Property<int>("Version")
                        .HasColumnType("int")
                        .HasColumnName("version");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("creation_time");

                    b.Property<string>("Payload")
                        .HasColumnType("longtext")
                        .HasColumnName("payload");

                    b.Property<string>("Type")
                        .HasColumnType("longtext")
                        .HasColumnName("type");

                    b.HasKey("SourcedId", "Version")
                        .HasName("pk___events");

                    b.ToTable("__events", (string)null);
                });

            modelBuilder.Entity("Notex.Infrastructure.EventSourcing.MementoEntity", b =>
                {
                    b.Property<Guid>("SourcedId")
                        .HasColumnType("char(36)")
                        .HasColumnName("sourced_id");

                    b.Property<int>("Version")
                        .HasColumnType("int")
                        .HasColumnName("version");

                    b.Property<string>("Payload")
                        .HasColumnType("longtext")
                        .HasColumnName("payload");

                    b.Property<string>("Type")
                        .HasColumnType("longtext")
                        .HasColumnName("type");

                    b.HasKey("SourcedId", "Version")
                        .HasName("pk___mementos");

                    b.ToTable("__mementos", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
