using Microsoft.EntityFrameworkCore;
using Yoma.Core.Infrastructure.Database.Core.Entities;
using Yoma.Core.Infrastructure.Database.Entity.Entities;
using Yoma.Core.Infrastructure.Database.Lookups.Entities;

namespace Yoma.Core.Infrastructure.Database.Context
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructors
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        #endregion

        #region Public Members
        public DbSet<User> User { get; set; }

        public DbSet<Organization> Organization { get; set; }

        public DbSet<UserSkill> UserSkills { get; set; }   

        public DbSet<OrganizationProviderType> OrganizationProviderTypes { get; set; }     

        public DbSet<Country> Country { get; set; }

        public DbSet<Gender> Gender { get; set; }

        public DbSet<ProviderType> ProviderType { get; set; }

        public DbSet<Skill> Skill { get; set; }

        public DbSet<S3Object> S3Object { get; set; }
        #endregion

        #region Protected Members
        protected override void OnModelCreating(ModelBuilder builder) { }
        #endregion
    }
}
