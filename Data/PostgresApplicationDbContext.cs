using Microsoft.EntityFrameworkCore;

namespace XMLDocCrowdSourcer.Data {
    // Add-Migration <MigrationName> -Context PostgresApplicationDbContext -OutputDir Data\Migrations\PostgresMigrations
    // Script-Migration [from] [to]
    public class PostgresApplicationDbContext(IConfiguration config) : ApplicationDbContext(config) {
        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseNpgsql(connectionString);
        }
    }
}
