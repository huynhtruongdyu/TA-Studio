using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Portal.Infrastructure.EntityTypeConfiguration.Auth;

namespace Portal.Infrastructure
{
    public interface IPortalContext
    {
        //DbSet<User> Users { get; set; }
        //Task<int> SaveChanges(CancellationToken cancellationToken = default);
    }

    public class PortalContext : DbContext, IPortalContext
    {
        public PortalContext(DbContextOptions<PortalContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
        }
    }

    public class PortalContextDesignFactory : IDesignTimeDbContextFactory<PortalContext>
    {
        public PortalContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=127.0.0.1;Port=5432;Database=ta-studio-dev;User Id=admin;Password=root;";
            //var connectionString = "Host:localhost;Port:5432;Database:ta-studo-dev;Username:admin;Password:root";
            var optionsBuilder = new DbContextOptionsBuilder<PortalContext>()
                .UseNpgsql(connectionString);

            return new PortalContext(optionsBuilder.Options);
        }
    }
}