using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace XMLDocCrowdSourcer.Data {
    // To update db schema, run in Package Manager Console:
    // Add-Migration <NameInPascalCase>
    // Update-Database
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<MappingGroup> MappingGroups { get; set; }
        public virtual DbSet<Mapping> Mappings { get; set; }
        public virtual DbSet<MappingProposal> MappingProposals { get; set; }
        public virtual DbSet<ProjectUser> ProjectUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            
        }
    }
}
