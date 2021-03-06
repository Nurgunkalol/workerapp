﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WorkersApp.Data;
using WorkersApp.Entities;

namespace WorkersApp.Migrations
{
    [DbContext(typeof(WorkersContext))]
    [Migration("20171028054256_WorkerMigration")]
    partial class WorkerMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("WorkersApp.Entities.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("BaseRate");

                    b.Property<int?>("ChiefId");

                    b.Property<DateTime>("EntryWorkDate");

                    b.Property<int>("Group");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("ChiefId");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("WorkersApp.Entities.Worker", b =>
                {
                    b.HasOne("WorkersApp.Entities.Worker", "Chief")
                        .WithMany()
                        .HasForeignKey("ChiefId");
                });
#pragma warning restore 612, 618
        }
    }
}
