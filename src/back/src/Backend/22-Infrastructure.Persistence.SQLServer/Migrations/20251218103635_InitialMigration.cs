using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.SQLServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppAction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppPermissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Constituencies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficialCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constituencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constituencies_Constituencies_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Constituencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeIso = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DialCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DisabledDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Civility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActionPermission",
                columns: table => new
                {
                    ActionsId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionPermission", x => new { x.ActionsId, x.PermissionsId });
                    table.ForeignKey(
                        name: "FK_ActionPermission_AppAction_ActionsId",
                        column: x => x.ActionsId,
                        principalTable: "AppAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionPermission_AppPermissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "AppPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ResidenceAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Locality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coordinates_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleAction",
                columns: table => new
                {
                    ActionsId = table.Column<long>(type: "bigint", nullable: false),
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAction", x => new { x.ActionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_RoleAction_AppAction_ActionsId",
                        column: x => x.ActionsId,
                        principalTable: "AppAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleAction_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Expiry = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsUserType = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Citizens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nni = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarriedName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    BirthDay = table.Column<DateOnly>(type: "date", nullable: false),
                    BirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CoordinateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdentityDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Citizens_Coordinates_CoordinateId",
                        column: x => x.CoordinateId,
                        principalTable: "Coordinates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Citizens_IdentityDocuments_IdentityDocumentId",
                        column: x => x.IdentityDocumentId,
                        principalTable: "IdentityDocuments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Electors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ElectorNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RegistrationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActif = table.Column<bool>(type: "bit", nullable: false),
                    DesactivationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CitizenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConstituencyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Electors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Electors_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Electors_Constituencies_ConstituencyId",
                        column: x => x.ConstituencyId,
                        principalTable: "Constituencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Filiations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CitoyenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelatifId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeLien = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Proof = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filiations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filiations_Citizens_CitoyenId",
                        column: x => x.CitoyenId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Filiations_Citizens_RelatifId",
                        column: x => x.RelatifId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    SoumissionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReasonForRejection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastUpdaterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CitizenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationRequests_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegistrationRequests_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrationRequests_Users_LastUpdaterId",
                        column: x => x.LastUpdaterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportingDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    UrlStockage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateUpload = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    SupportingDocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportingDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportingDocuments_RegistrationRequests_RegistrationRequestId",
                        column: x => x.RegistrationRequestId,
                        principalTable: "RegistrationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CodeIso", "DialCode", "Name" },
                values: new object[,]
                {
                    { 1L, "AF", "+93", "Afghanistan" },
                    { 2L, "ZA", "+27", "Afrique du Sud" },
                    { 3L, "AL", "+355", "Albanie" },
                    { 4L, "DZ", "+213", "Algérie" },
                    { 5L, "DE", "+49", "Allemagne" },
                    { 6L, "AD", "+376", "Andorre" },
                    { 7L, "AO", "+244", "Angola" },
                    { 8L, "AI", "+1264", "Anguilla" },
                    { 9L, "AQ", "672", "Antarctique" },
                    { 10L, "AG", "+1268", "Antigua-et-Barbuda" },
                    { 11L, "SA", "+966", "Arabie saoudite" },
                    { 12L, "AR", "+54", "Argentine" },
                    { 13L, "AM", "+374", "Arménie" },
                    { 14L, "AW", "+297", "Aruba" },
                    { 15L, "AU", "+61", "Australie" },
                    { 16L, "AT", "+43", "Autriche" },
                    { 17L, "AZ", "+994", "Azerbaïdjan" },
                    { 18L, "BS", "+1242", "Bahamas" },
                    { 19L, "BH", "+973", "Bahreïn" },
                    { 20L, "BD", "+880", "Bangladesh" },
                    { 21L, "BB", "+1246", "Barbade" },
                    { 22L, "BE", "+32", "Belgique" },
                    { 23L, "BZ", "+501", "Belize" },
                    { 24L, "BJ", "+229", "Bénin" },
                    { 25L, "BM", "+1441", "Bermudes" },
                    { 26L, "BT", "+975", "Bhoutan" },
                    { 27L, "BY", "+375", "Biélorussie" },
                    { 28L, "BO", "+591", "Bolivie" },
                    { 29L, "BA", "+387", "Bosnie-Herzégovine" },
                    { 30L, "BW", "+267", "Botswana" },
                    { 31L, "BR", "+55", "Brésil" },
                    { 32L, "BN", "+673", "Brunei" },
                    { 33L, "BG", "+359", "Bulgarie" },
                    { 34L, "BF", "+226", "Burkina Faso" },
                    { 35L, "BI", "+257", "Burundi" },
                    { 36L, "KH", "+855", "Cambodge" },
                    { 37L, "CM", "+237", "Cameroun" },
                    { 38L, "CA", "+1", "Canada" },
                    { 39L, "CV", "+238", "Cap-Vert" },
                    { 40L, "CL", "+56", "Chili" },
                    { 41L, "CN", "+86", "Chine" },
                    { 42L, "CY", "+357", "Chypre" },
                    { 43L, "CO", "+57", "Colombie" },
                    { 44L, "KM", "+269", "Comores" },
                    { 45L, "CG", "+242", "Congo" },
                    { 46L, "CD", "+243", "Congo (RDC)" },
                    { 47L, "KP", "+850", "Corée du Nord" },
                    { 48L, "KR", "+82", "Corée du Sud" },
                    { 49L, "CR", "+506", "Costa Rica" },
                    { 50L, "CI", "+225", "Côte d’Ivoire" },
                    { 51L, "HR", "+385", "Croatie" },
                    { 52L, "CU", "+53", "Cuba" },
                    { 53L, "DK", "+45", "Danemark" },
                    { 54L, "DJ", "+253", "Djibouti" },
                    { 55L, "DM", "+1467", "Dominique" },
                    { 56L, "EG", "+20", "Égypte" },
                    { 57L, "AE", "+971", "Émirats arabes unis" },
                    { 58L, "EC", "+593", "Équateur" },
                    { 59L, "ER", "+291", "Érythrée" },
                    { 60L, "ES", "+34", "Espagne" },
                    { 61L, "EE", "+372", "Estonie" },
                    { 62L, "US", "+1", "États-Unis" },
                    { 63L, "ET", "+251", "Éthiopie" },
                    { 64L, "FJ", "+679", "Fidji" },
                    { 65L, "FI", "+358", "Finlande" },
                    { 66L, "FR", "+33", "France" },
                    { 67L, "GA", "+241", "Gabon" },
                    { 68L, "GM", "+220", "Gambie" },
                    { 69L, "GH", "+233", "Ghana" },
                    { 70L, "GR", "+30", "Grèce" },
                    { 71L, "GD", "+1473", "Grenade" },
                    { 72L, "GT", "+502", "Guatemala" },
                    { 73L, "GN", "+224", "Guinée" },
                    { 74L, "GQ", "+240", "Guinée équatoriale" },
                    { 75L, "GW", "+245", "Guinée-Bissau" },
                    { 76L, "GY", "+592", "Guyana" },
                    { 77L, "HT", "+509", "Haïti" },
                    { 78L, "HN", "+504", "Honduras" },
                    { 79L, "HU", "+36", "Hongrie" },
                    { 80L, "IN", "+91", "Inde" },
                    { 81L, "ID", "+62", "Indonésie" },
                    { 82L, "IQ", "+964", "Irak" },
                    { 83L, "IR", "+98", "Iran" },
                    { 84L, "IE", "+353", "Irlande" },
                    { 85L, "IS", "+354", "Islande" },
                    { 86L, "IL", "+972", "Israël" },
                    { 87L, "IT", "+39", "Italie" },
                    { 88L, "JM", "+1876", "Jamaïque" },
                    { 89L, "JP", "+81", "Japon" },
                    { 90L, "JO", "+962", "Jordanie" },
                    { 91L, "KZ", "+997", "Kazakhstan" },
                    { 92L, "KE", "+254", "Kenya" },
                    { 93L, "KG", "+996", "Kirghizistan" },
                    { 94L, "KI", "+686", "Kiribati" },
                    { 95L, "KW", "+965", "Koweït" },
                    { 96L, "LA", "+856", "Laos" },
                    { 97L, "LS", "+266", "Lesotho" },
                    { 98L, "LV", "+371", "Lettonie" },
                    { 99L, "LB", "+961", "Liban" },
                    { 100L, "LR", "+231", "Libéria" },
                    { 101L, "LY", "+218", "Libye" },
                    { 102L, "LI", "+423", "Liechtenstein" },
                    { 103L, "LT", "+370", "Lituanie" },
                    { 104L, "LU", "+352", "Luxembourg" },
                    { 105L, "MK", "+389", "Macédoine du Nord" },
                    { 106L, "MG", "+261", "Madagascar" },
                    { 107L, "MY", "+60", "Malaisie" },
                    { 108L, "MW", "+265", "Malawi" },
                    { 109L, "MV", "+960", "Maldives" },
                    { 110L, "ML", "+223", "Mali" },
                    { 111L, "MT", "+356", "Malte" },
                    { 112L, "MA", "+212", "Maroc" },
                    { 113L, "MU", "+230", "Maurice" },
                    { 114L, "MR", "+222", "Mauritanie" },
                    { 115L, "MX", "+52", "Mexique" },
                    { 116L, "FM", "+691", "Micronésie" },
                    { 117L, "MD", "+373", "Moldavie" },
                    { 118L, "MC", "+377", "Monaco" },
                    { 119L, "MN", "+976", "Mongolie" },
                    { 120L, "ME", "+382", "Monténégro" },
                    { 121L, "MZ", "+258", "Mozambique" },
                    { 122L, "NA", "+264", "Namibie" },
                    { 123L, "NR", "+674", "Nauru" },
                    { 124L, "NP", "+977", "Népal" },
                    { 125L, "NI", "+505", "Nicaragua" },
                    { 126L, "NE", "+227", "Niger" },
                    { 127L, "NG", "+234", "Nigeria" },
                    { 128L, "NO", "+47", "Norvège" },
                    { 129L, "NZ", "+64", "Nouvelle-Zélande" },
                    { 130L, "OM", "+968", "Oman" },
                    { 131L, "UG", "+256", "Ouganda" },
                    { 132L, "UZ", "+998", "Ouzbékistan" },
                    { 133L, "PK", "+92", "Pakistan" },
                    { 134L, "PW", "+680", "Palaos" },
                    { 135L, "PS", "+970", "Palestine" },
                    { 136L, "PA", "+507", "Panama" },
                    { 137L, "PG", "+675", "Papouasie-Nouvelle-Guinée" },
                    { 138L, "PY", "+595", "Paraguay" },
                    { 139L, "NL", "+31", "Pays-Bas" },
                    { 140L, "PE", "+63", "Pérou" },
                    { 141L, "PH", "+63", "Philippines" },
                    { 142L, "PL", "+48", "Pologne" },
                    { 143L, "PT", "+351", "Portugal" },
                    { 144L, "QA", "+974", "Qatar" },
                    { 145L, "RO", "+40", "Roumanie" },
                    { 146L, "GB", "+44", "Royaume-Uni" },
                    { 147L, "RU", "+7", "Russie" },
                    { 148L, "RW", "+250", "Rwanda" },
                    { 149L, "LC", "+1-758", "Sainte-Lucie" },
                    { 150L, "VC", "+1-784", "Saint-Vincent-et-les-Grenadines" },
                    { 151L, "WS", "+685", "Samoa" },
                    { 152L, "SM", "+590", "Saint-Marin" },
                    { 153L, "ST", "+239", "Sao Tomé-et-Principe" },
                    { 154L, "SN", "+221", "Sénégal" },
                    { 155L, "RS", "+381", "Serbie" },
                    { 156L, "SC", "+248", "Seychelles" },
                    { 157L, "SL", "+232", "Sierra Leone" },
                    { 158L, "SG", "+65", "Singapour" },
                    { 159L, "SK", "+421", "Slovaquie" },
                    { 160L, "SI", "+386", "Slovénie" },
                    { 161L, "SO", "+252", "Somalie" },
                    { 162L, "SD", "+249", "Soudan" },
                    { 163L, "SS", "+211", "Soudan du Sud" },
                    { 164L, "LK", "+94", "Sri Lanka" },
                    { 165L, "SE", "+46", "Suède" },
                    { 166L, "CH", "+41", "Suisse" },
                    { 167L, "SR", "+597", "Suriname" },
                    { 168L, "SY", "+963", "Syrie" },
                    { 169L, "TJ", "+992", "Tadjikistan" },
                    { 170L, "TZ", "+255", "Tanzanie" },
                    { 171L, "TD", "+235", "Tchad" },
                    { 172L, "CZ", "+420", "Tchéquie" },
                    { 173L, "TH", "+66", "Thaïlande" },
                    { 174L, "TL", "+670", "Timor oriental" },
                    { 175L, "TG", "+228", "Togo" },
                    { 176L, "TO", "+676", "Tonga" },
                    { 177L, "TT", "+1-868", "Trinité-et-Tobago" },
                    { 178L, "TN", "+216", "Tunisie" },
                    { 179L, "TM", "+993", "Turkménistan" },
                    { 180L, "TR", "+90", "Turquie" },
                    { 181L, "TV", "+688", "Tuvalu" },
                    { 182L, "UA", "+380", "Ukraine" },
                    { 183L, "UY", "+598", "Uruguay" },
                    { 184L, "VU", "+678", "Vanuatu" },
                    { 185L, "VE", "+58", "Venezuela" },
                    { 186L, "VN", "+84", "Viêt Nam" },
                    { 187L, "YE", "+967", "Yémen" },
                    { 188L, "ZM", "+260", "Zambie" },
                    { 189L, "ZW", "+263", "Zimbabwe" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionPermission_PermissionsId",
                table: "ActionPermission",
                column: "PermissionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Citizens_CoordinateId",
                table: "Citizens",
                column: "CoordinateId");

            migrationBuilder.CreateIndex(
                name: "IX_Citizens_IdentityDocumentId",
                table: "Citizens",
                column: "IdentityDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Constituencies_ParentId",
                table: "Constituencies",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Coordinates_CountryId",
                table: "Coordinates",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Electors_CitizenId",
                table: "Electors",
                column: "CitizenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Electors_ConstituencyId",
                table: "Electors",
                column: "ConstituencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Electors_ElectorNumber",
                table: "Electors",
                column: "ElectorNumber",
                unique: true,
                filter: "[ElectorNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Filiations_CitoyenId_RelatifId_TypeLien",
                table: "Filiations",
                columns: new[] { "CitoyenId", "RelatifId", "TypeLien" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Filiations_RelatifId",
                table: "Filiations",
                column: "RelatifId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationRequests_AuthorId",
                table: "RegistrationRequests",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationRequests_CitizenId",
                table: "RegistrationRequests",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationRequests_LastUpdaterId",
                table: "RegistrationRequests",
                column: "LastUpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAction_RolesId",
                table: "RoleAction",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SupportingDocuments_RegistrationRequestId",
                table: "SupportingDocuments",
                column: "RegistrationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionPermission");

            migrationBuilder.DropTable(
                name: "Electors");

            migrationBuilder.DropTable(
                name: "Filiations");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "RoleAction");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "SupportingDocuments");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "AppPermissions");

            migrationBuilder.DropTable(
                name: "Constituencies");

            migrationBuilder.DropTable(
                name: "AppAction");

            migrationBuilder.DropTable(
                name: "RegistrationRequests");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Citizens");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Coordinates");

            migrationBuilder.DropTable(
                name: "IdentityDocuments");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
