using Application.Common.Enums;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.SQLServer.Contexts.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.SQLServer.Contexts
{
    public class ApplicationDbContext
        : IdentityDbContext<
            UserDao,
            RoleDao,
            Guid,
            IdentityUserClaim<Guid>,
            UserRoleDao,
            IdentityUserLogin<Guid>,
            IdentityRoleClaim<Guid>,
            IdentityUserToken<Guid>
        >
    {
        public DbSet<AppActionDao> AppActions { get; set; }
        public DbSet<AppPermissionDao> AppPermissions { get; set; }
        public DbSet<CitizenDao> Citizens { get; set; }
        public DbSet<CountryDao> Countries { get; set; }
        public DbSet<ConstituencyDao> Constituencies { get; set; }
        public DbSet<CoordinateDao> Coordinates { get; set; }
        public DbSet<FiliationDao> Filiations { get; set; }
        public DbSet<IdentityDocumentDao> IdentityDocuments { get; set; }
        public DbSet<RegistrationRequestDao> RegistrationRequests { get; set; }
        public DbSet<ElectorDao> Electors { get; set; }
        public DbSet<SupportingDocumentsDao> SupportingDocuments { get; set; }
        public DbSet<RefreshTokenDao> RefreshTokens { get; set; }

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<PersonTitle>()
                .HaveConversion<EnumToStringConverter<PersonTitle>>();

            configurationBuilder
                .Properties<AppAction>()
                .HaveConversion<EnumToStringConverter<AppAction>>();
            configurationBuilder
                .Properties<TypeOfRelationship>()
                .HaveConversion<EnumToStringConverter<TypeOfRelationship>>();
            configurationBuilder
                .Properties<RegistrationStatus>()
                .HaveConversion<EnumToStringConverter<RegistrationStatus>>();
            configurationBuilder
                .Properties<RegistrationRequestType>()
                .HaveConversion<EnumToStringConverter<RegistrationRequestType>>();
            configurationBuilder
                .Properties<MaritalStatus>()
                .HaveConversion<EnumToStringConverter<MaritalStatus>>();
            configurationBuilder
                .Properties<Gender>()
                .HaveConversion<EnumToStringConverter<Gender>>();
            configurationBuilder
                .Properties<DocumentType>()
                .HaveConversion<EnumToStringConverter<DocumentType>>();
            configurationBuilder
                .Properties<ConstituencyType>()
                .HaveConversion<EnumToStringConverter<ConstituencyType>>();
            configurationBuilder
                .Properties<AppPermission>()
                .HaveConversion<EnumToStringConverter<AppPermission>>();
            configurationBuilder
                .Properties<UserType>()
                .HaveConversion<EnumToStringConverter<UserType>>();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserDao>().ToTable("Users");
            builder.Entity<RoleDao>().ToTable("Roles");
            builder.Entity<UserRoleDao>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

            builder.Entity<UserDao>().Property(e => e.UserName).HasMaxLength(100);
            builder.Entity<UserDao>().Property(e => e.FirstName).HasMaxLength(100);
            builder.Entity<UserDao>().Property(e => e.LastName).HasMaxLength(100);
            builder.Entity<UserDao>().Property(e => e.Email).HasMaxLength(100);
            builder
                .Entity<UserDao>()
                .HasMany(u => u.RefreshTokens)
                .WithOne(r => r.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<UserDao>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<UserRoleDao>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<UserRoleDao>()
                .HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<RoleDao>()
                .HasMany(e => e.Actions)
                .WithMany(e => e.Roles)
                .UsingEntity(x => x.ToTable("RoleAction"));

            builder
                .Entity<AppActionDao>()
                .HasMany(e => e.Permissions)
                .WithMany(e => e.Actions)
                .UsingEntity(j => j.ToTable("ActionPermission"));

            builder
                .Entity<RegistrationRequestDao>()
                .HasOne(l => l.Author)
                .WithMany(a => a.CreatedRegistrationRequests)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<RegistrationRequestDao>()
                .HasOne(l => l.LastUpdater)
                .WithMany(a => a.UpdatedRegistrationRequests)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ElectorDao>().HasIndex(e => e.ElectorNumber).IsUnique();

            builder
                .Entity<ElectorDao>()
                .HasOne(e => e.Citizen)
                .WithOne(c => c.Elector)
                .HasForeignKey<ElectorDao>(e => e.CitizenId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Filiation : deux relations vers Citizen, bien distinctes
            builder.Entity<FiliationDao>(entity =>
            {
                entity
                    .HasOne(f => f.Citoyen)
                    .WithMany(c => c.FiliationsAsSubject)
                    .HasForeignKey(f => f.CitoyenId)
                    .OnDelete(DeleteBehavior.Restrict); // éviter cascades cycliques

                entity
                    .HasOne(f => f.Relatif)
                    .WithMany(c => c.FiliationsAsRelative)
                    .HasForeignKey(f => f.RelatifId)
                    .OnDelete(DeleteBehavior.Restrict); // idem
            });

            // (Fortement conseillé) Index/contrainte d’unicité logique pour éviter doublons
            builder
                .Entity<FiliationDao>()
                .HasIndex(f => new
                {
                    f.CitoyenId,
                    f.RelatifId,
                    f.TypeLien,
                })
                .IsUnique();

            //Seeding
            builder.Entity<CountryDao>().HasData(CountriesData.Countries);
        }
    }
}
