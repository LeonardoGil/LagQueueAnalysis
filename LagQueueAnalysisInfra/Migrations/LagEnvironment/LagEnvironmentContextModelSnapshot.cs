﻿// <auto-generated />
using System;
using LagQueueAnalysisInfra.EFContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LagQueueAnalysisInfra.Migrations.LagEnvironment
{
    [DbContext(typeof(LagEnvironmentContext))]
    partial class LagEnvironmentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LagEnvironmentDomain.Entities.AnalysisEnvironment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Database")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Environments");
                });

            modelBuilder.Entity("LagEnvironmentDomain.Entities.Token", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnalysisEnvironmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnalysisEnvironmentId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("LagEnvironmentDomain.Entities.Token", b =>
                {
                    b.HasOne("LagEnvironmentDomain.Entities.AnalysisEnvironment", "AnalysisEnvironment")
                        .WithMany()
                        .HasForeignKey("AnalysisEnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnalysisEnvironment");
                });
#pragma warning restore 612, 618
        }
    }
}
