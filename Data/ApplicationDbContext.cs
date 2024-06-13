using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace XMLDocCrowdSourcer.Data {
    // To update db schema, run in Package Manager Console:
    // Add-Migration <NameInPascalCase>
    // Update-Database
    public class ApplicationDbContext(IConfiguration config) : IdentityDbContext<ApplicationUser>() {
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<MappingGroup> MappingGroups { get; set; }
        public virtual DbSet<Mapping> Mappings { get; set; }
        public virtual DbSet<MappingProposal> MappingProposals { get; set; }
        public virtual DbSet<ProjectUser> ProjectUsers { get; set; }

        protected string connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            base.OnConfiguring(options);
            options.UseSqlServer(connectionString);
        }
    }
}
