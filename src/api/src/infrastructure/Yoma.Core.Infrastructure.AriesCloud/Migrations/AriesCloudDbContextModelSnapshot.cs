﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Yoma.Core.Infrastructure.AriesCloud.Context;

#nullable disable

namespace Yoma.Core.Infrastructure.AriesCloud.Migrations
{
    [DbContext(typeof(AriesCloudDbContext))]
    partial class AriesCloudDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Yoma.Core.Infrastructure.AriesCloud.Entities.Connection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Protocol")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<string>("SourceConnectionId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("SourceTenantId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TargetConnectionId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TargetTenantId")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("SourceTenantId", "TargetTenantId", "Protocol")
                        .IsUnique();

                    b.ToTable("Connection", "AriesCloud");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.AriesCloud.Entities.CredentialSchema", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(125)");

                    b.Property<string>("ArtifactType")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("AttributeNames")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(125)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Name", "ArtifactType");

                    b.ToTable("CredentialSchema", "AriesCloud");
                });
#pragma warning restore 612, 618
        }
    }
}
