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
        public DbSet<CitizenDao> Citizens { get; set; }
        public DbSet<CountryDao> Countries { get; set; }
        public DbSet<ConstituencyDao> Constituencies { get; set; }
        public DbSet<CoordinateDao> Coordinates { get; set; }
        public DbSet<FiliationDao> Filiations { get; set; }
        public DbSet<IdentityDocumentDao> IdentityDocuments { get; set; }
        public DbSet<RegistrationRequestDao> RegistrationRequests { get; set; }
        public DbSet<VoterDao> Voters { get; set; }
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
                .Properties<UserType>()
                .HaveConversion<EnumToStringConverter<UserType>>();
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
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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

            //Seeding
            builder.Entity<CountryDao>().HasData(CountriesData.Countries);
        }
    }
}
