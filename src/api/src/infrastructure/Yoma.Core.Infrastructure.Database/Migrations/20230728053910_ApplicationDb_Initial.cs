using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yoma.Core.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationDb_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lookup");

            migrationBuilder.EnsureSchema(
                name: "object");

            migrationBuilder.EnsureSchema(
                name: "entity");

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", nullable: false),
                    CodeAlpha2 = table.Column<string>(type: "varchar(2)", nullable: false),
                    CodeAlpha3 = table.Column<string>(type: "varchar(3)", nullable: false),
                    CodeNumeric = table.Column<string>(type: "varchar(3)", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "File",
                schema: "object",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObjectKey = table.Column<string>(type: "varchar(125)", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProviderType",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "entity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "varchar(320)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(125)", nullable: false),
                    Surname = table.Column<string>(type: "varchar(125)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(50)", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CountryOfResidenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DateLastLogin = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ZltoWalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ZltoWalletCountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "lookup",
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Country_CountryOfResidenceId",
                        column: x => x.CountryOfResidenceId,
                        principalSchema: "lookup",
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Country_ZltoWalletCountryId",
                        column: x => x.ZltoWalletCountryId,
                        principalSchema: "lookup",
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_File_PhotoId",
                        column: x => x.PhotoId,
                        principalSchema: "object",
                        principalTable: "File",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Gender_GenderId",
                        column: x => x.GenderId,
                        principalSchema: "lookup",
                        principalTable: "Gender",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                schema: "entity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    WebsiteURL = table.Column<string>(type: "varchar(2048)", nullable: true),
                    PrimaryContactName = table.Column<string>(type: "varchar(255)", nullable: true),
                    PrimaryContactEmail = table.Column<string>(type: "varchar(320)", nullable: true),
                    PrimaryContactPhone = table.Column<string>(type: "varchar(50)", nullable: true),
                    VATIN = table.Column<string>(type: "varchar(255)", nullable: true),
                    TaxNumber = table.Column<string>(type: "varchar(255)", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "varchar(255)", nullable: true),
                    City = table.Column<string>(type: "varchar(50)", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StreetAddress = table.Column<string>(type: "varchar(500)", nullable: true),
                    Province = table.Column<string>(type: "varchar(255)", nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    Tagline = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Biography = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    DateApproved = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateDeactivated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyRegistrationDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organization_Country_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "lookup",
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Organization_File_CompanyRegistrationDocumentId",
                        column: x => x.CompanyRegistrationDocumentId,
                        principalSchema: "object",
                        principalTable: "File",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Organization_File_LogoId",
                        column: x => x.LogoId,
                        principalSchema: "object",
                        principalTable: "File",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Organization_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "entity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserSkills",
                schema: "entity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSkills_Skill_SkillId",
                        column: x => x.SkillId,
                        principalSchema: "lookup",
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkills_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "entity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationProviderTypes",
                schema: "entity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationProviderTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationProviderTypes_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "entity",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationProviderTypes_ProviderType_ProviderTypeId",
                        column: x => x.ProviderTypeId,
                        principalSchema: "lookup",
                        principalTable: "ProviderType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_Name",
                schema: "lookup",
                table: "Country",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_File_ObjectKey",
                schema: "object",
                table: "File",
                column: "ObjectKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gender_Name",
                schema: "lookup",
                table: "Gender",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organization_CompanyRegistrationDocumentId",
                schema: "entity",
                table: "Organization",
                column: "CompanyRegistrationDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_CountryId",
                schema: "entity",
                table: "Organization",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_LogoId",
                schema: "entity",
                table: "Organization",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_Name",
                schema: "entity",
                table: "Organization",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organization_UserId_Approved_Active_DateModified_DateCreated",
                schema: "entity",
                table: "Organization",
                columns: new[] { "UserId", "Approved", "Active", "DateModified", "DateCreated" });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProviderTypes_OrganizationId_ProviderTypeId",
                schema: "entity",
                table: "OrganizationProviderTypes",
                columns: new[] { "OrganizationId", "ProviderTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProviderTypes_ProviderTypeId",
                schema: "entity",
                table: "OrganizationProviderTypes",
                column: "ProviderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderType_Name",
                schema: "lookup",
                table: "ProviderType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Name",
                schema: "lookup",
                table: "Skill",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_CountryId",
                schema: "entity",
                table: "User",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CountryOfResidenceId",
                schema: "entity",
                table: "User",
                column: "CountryOfResidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "entity",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_FirstName_Surname_PhoneNumber_ExternalId_DateCreated_DateModified",
                schema: "entity",
                table: "User",
                columns: new[] { "FirstName", "Surname", "PhoneNumber", "ExternalId", "DateCreated", "DateModified" });

            migrationBuilder.CreateIndex(
                name: "IX_User_GenderId",
                schema: "entity",
                table: "User",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PhotoId",
                schema: "entity",
                table: "User",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ZltoWalletCountryId",
                schema: "entity",
                table: "User",
                column: "ZltoWalletCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_SkillId",
                schema: "entity",
                table: "UserSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_UserId_SkillId",
                schema: "entity",
                table: "UserSkills",
                columns: new[] { "UserId", "SkillId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationProviderTypes",
                schema: "entity");

            migrationBuilder.DropTable(
                name: "UserSkills",
                schema: "entity");

            migrationBuilder.DropTable(
                name: "Organization",
                schema: "entity");

            migrationBuilder.DropTable(
                name: "ProviderType",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "Skill",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "User",
                schema: "entity");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "File",
                schema: "object");

            migrationBuilder.DropTable(
                name: "Gender",
                schema: "lookup");
        }
    }
}
