﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Yoma.Core.Infrastructure.Database.Context;

#nullable disable

namespace Yoma.Core.Infrastructure.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Core.Entities.S3Object", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ObjectKey")
                        .IsRequired()
                        .HasColumnType("varchar(125)");

                    b.HasKey("Id");

                    b.HasIndex("ObjectKey")
                        .IsUnique();

                    b.ToTable("File", "object");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Entity.Entities.Lookups.OrganizationProviderType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("OrganizationProviderType", "entity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Entity.Entities.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<bool>("Approved")
                        .HasColumnType("bit");

                    b.Property<string>("Biography")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("City")
                        .HasColumnType("varchar(50)");

                    b.Property<Guid?>("CompanyRegistrationDocumentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("DateApproved")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeactivated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("LogoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("PrimaryContactEmail")
                        .HasColumnType("varchar(320)");

                    b.Property<string>("PrimaryContactName")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PrimaryContactPhone")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Province")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Tagline")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("TaxNumber")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("VATIN")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("WebsiteURL")
                        .HasColumnType("varchar(2048)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyRegistrationDocumentId");

                    b.HasIndex("CountryId");

                    b.HasIndex("LogoId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Approved", "Active", "DateModified", "DateCreated");

                    b.ToTable("Organization", "entity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Entity.Entities.OrganizationProviderType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProviderTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProviderTypeId");

                    b.HasIndex("OrganizationId", "ProviderTypeId")
                        .IsUnique();

                    b.ToTable("OrganizationProviderTypes", "entity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Entity.Entities.OrganizationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("OrganizationId", "UserId")
                        .IsUnique();

                    b.ToTable("OrganizationUsers", "entity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Entity.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CountryOfResidenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateLastLogin")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(320)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(125)");

                    b.Property<Guid?>("GenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(50)");

                    b.Property<Guid?>("PhotoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("varchar(125)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ZltoWalletCountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ZltoWalletId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("CountryOfResidenceId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("GenderId");

                    b.HasIndex("PhotoId");

                    b.HasIndex("ZltoWalletCountryId");

                    b.HasIndex("FirstName", "Surname", "PhoneNumber", "ExternalId", "DateCreated", "DateModified");

                    b.ToTable("User", "entity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Entity.Entities.UserSkill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("SkillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SkillId");

                    b.HasIndex("UserId", "SkillId")
                        .IsUnique();

                    b.ToTable("UserSkills", "entity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Lookups.Entities.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodeAlpha2")
                        .IsRequired()
                        .HasColumnType("varchar(2)");

                    b.Property<string>("CodeAlpha3")
                        .IsRequired()
                        .HasColumnType("varchar(3)");

                    b.Property<string>("CodeNumeric")
                        .IsRequired()
                        .HasColumnType("varchar(3)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(125)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Country", "lookup");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Lookups.Entities.Gender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Gender", "lookup");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Lookups.Entities.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodeAlpha2")
                        .IsRequired()
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(125)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Language", "lookup");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Lookups.Entities.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Skill", "lookup");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Lookups.Entities.TimeInterval", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("TimeInterval", "lookup");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Opportunity.Entities.Lookups.OpportunityCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(125)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("OpportunityCategory", "opportunity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Opportunity.Entities.Lookups.OpportunityDifficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("OpportunityDifficulty", "opportunity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Opportunity.Entities.Lookups.OpportunityStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("OpportunityStatus", "opportunity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Opportunity.Entities.Lookups.OpportunityType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("OpportunityType", "opportunity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Opportunity.Entities.Opportunity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<short?>("CommitmentIntervalCount")
                        .HasColumnType("smallint");

                    b.Property<Guid>("CommitmentIntervalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateStart")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Instructions")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("Keywords")
                        .HasColumnType("varchar(500)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ParticipantCount")
                        .HasColumnType("int");

                    b.Property<int?>("ParticipantLimit")
                        .HasColumnType("int");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("URL")
                        .HasColumnType("varchar(2048)");

                    b.Property<bool>("VerificationSupported")
                        .HasColumnType("bit");

                    b.Property<decimal?>("YomaReward")
                        .HasColumnType("decimal(8,2)");

                    b.Property<decimal?>("YomaRewardPool")
                        .HasColumnType("decimal(8,2)");

                    b.Property<decimal?>("ZltoReward")
                        .HasColumnType("decimal(8,2)");

                    b.Property<decimal?>("ZltoRewardPool")
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("Id");

                    b.HasIndex("CommitmentIntervalId");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("StatusId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.HasIndex("TypeId", "OrganizationId", "DifficultyId", "CommitmentIntervalId", "StatusId", "Keywords", "DateStart", "DateEnd", "DateCreated", "DateModified");

                    b.ToTable("Opportunity", "opportunity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Opportunity.Entities.OpportunityCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("OpportunityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OpportunityId", "CategoryId")
                        .IsUnique();

                    b.ToTable("OpportunityCategories", "opportunity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Opportunity.Entities.OpportunityCountry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("OpportunityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("OpportunityId", "CountryId")
                        .IsUnique();

                    b.ToTable("OpportunityCountries", "opportunity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Opportunity.Entities.OpportunityLanguage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OpportunityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("OpportunityId", "LanguageId")
                        .IsUnique();

                    b.ToTable("OpportunityLanguages", "opportunity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Opportunity.Entities.OpportunitySkill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("OpportunityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SkillId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SkillId");

                    b.HasIndex("OpportunityId", "SkillId")
                        .IsUnique();

                    b.ToTable("OpportunitySkill", "opportunity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Entity.Entities.Organization", b =>
                {
                    b.HasOne("Yoma.Core.Infrastructure.Database.Core.Entities.S3Object", "CompanyRegistrationDocument")
                        .WithMany()
                        .HasForeignKey("CompanyRegistrationDocumentId");

                    b.HasOne("Yoma.Core.Infrastructure.Database.Lookups.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("Yoma.Core.Infrastructure.Database.Core.Entities.S3Object", "Logo")
                        .WithMany()
                        .HasForeignKey("LogoId");

                    b.Navigation("CompanyRegistrationDocument");

                    b.Navigation("Country");

                    b.Navigation("Logo");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Entity.Entities.OrganizationProviderType", b =>
                {
                    b.HasOne("Yoma.Core.Infrastructure.Database.Entity.Entities.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yoma.Core.Infrastructure.Database.Entity.Entities.Lookups.OrganizationProviderType", "ProviderType")
                        .WithMany()
                        .HasForeignKey("ProviderTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("ProviderType");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Entity.Entities.OrganizationUser", b =>
                {
                    b.HasOne("Yoma.Core.Infrastructure.Database.Entity.Entities.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yoma.Core.Infrastructure.Database.Entity.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Entity.Entities.User", b =>
                {
                    b.HasOne("Yoma.Core.Infrastructure.Database.Lookups.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("Yoma.Core.Infrastructure.Database.Lookups.Entities.Country", "CountryOfResidence")
                        .WithMany()
                        .HasForeignKey("CountryOfResidenceId");

                    b.HasOne("Yoma.Core.Infrastructure.Database.Lookups.Entities.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId");

                    b.HasOne("Yoma.Core.Infrastructure.Database.Core.Entities.S3Object", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoId");

                    b.HasOne("Yoma.Core.Infrastructure.Database.Lookups.Entities.Country", "ZltoWalletCountry")
                        .WithMany()
                        .HasForeignKey("ZltoWalletCountryId");

                    b.Navigation("Country");

                    b.Navigation("CountryOfResidence");

                    b.Navigation("Gender");

                    b.Navigation("Photo");

                    b.Navigation("ZltoWalletCountry");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Entity.Entities.UserSkill", b =>
                {
                    b.HasOne("Yoma.Core.Infrastructure.Database.Lookups.Entities.Skill", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yoma.Core.Infrastructure.Database.Entity.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Opportunity.Entities.Opportunity", b =>
                {
                    b.HasOne("Yoma.Core.Infrastructure.Database.Lookups.Entities.TimeInterval", "CommitmentInterval")
                        .WithMany()
                        .HasForeignKey("CommitmentIntervalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yoma.Core.Infrastructure.Database.Opportunity.Entities.Lookups.OpportunityDifficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yoma.Core.Infrastructure.Database.Entity.Entities.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yoma.Core.Infrastructure.Database.Opportunity.Entities.Lookups.OpportunityStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yoma.Core.Infrastructure.Database.Opportunity.Entities.Lookups.OpportunityType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommitmentInterval");

                    b.Navigation("Difficulty");

                    b.Navigation("Organization");

                    b.Navigation("Status");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Opportunity.Entities.OpportunityCategory", b =>
                {
                    b.HasOne("Yoma.Core.Infrastructure.Database.Opportunity.Entities.Lookups.OpportunityCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yoma.Core.Infrastructure.Database.Opportunity.Entities.Opportunity", "Opportunity")
                        .WithMany()
                        .HasForeignKey("OpportunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Opportunity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Opportunity.Entities.OpportunityCountry", b =>
                {
                    b.HasOne("Yoma.Core.Infrastructure.Database.Lookups.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yoma.Core.Infrastructure.Database.Opportunity.Entities.Opportunity", "Opportunity")
                        .WithMany()
                        .HasForeignKey("OpportunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Opportunity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Opportunity.Entities.OpportunityLanguage", b =>
                {
                    b.HasOne("Yoma.Core.Infrastructure.Database.Lookups.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yoma.Core.Infrastructure.Database.Opportunity.Entities.Opportunity", "Opportunity")
                        .WithMany()
                        .HasForeignKey("OpportunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Opportunity");
                });

            modelBuilder.Entity("Yoma.Core.Infrastructure.Database.Opportunity.Entities.OpportunitySkill", b =>
                {
                    b.HasOne("Yoma.Core.Infrastructure.Database.Opportunity.Entities.Opportunity", "Opportunity")
                        .WithMany()
                        .HasForeignKey("OpportunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yoma.Core.Infrastructure.Database.Lookups.Entities.Skill", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Opportunity");

                    b.Navigation("Skill");
                });
#pragma warning restore 612, 618
        }
    }
}
