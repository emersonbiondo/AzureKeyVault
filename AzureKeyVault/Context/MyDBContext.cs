using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using AzureKeyVault.Configurations;

namespace AzureKeyVault.Context
{
    public class MyDBContext : DbContext
    {
        private const int DB_TIMEOUT = 3000;

        private static string? ConnectionString { get; set; }

        public MyDBContext()
        {
            Database.SetCommandTimeout(DB_TIMEOUT);
        }

        public MyDBContext(DbContextOptions<DbContext> options)
            : base(options)
        {
            Database.SetCommandTimeout(DB_TIMEOUT);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configurations = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IConfiguration).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => Activator.CreateInstance(x) as IConfiguration).ToList();

            foreach (var configuration in configurations)
            {
                if (configuration != null)
                {
                    configuration.ModelCreating(modelBuilder);
                }
            }
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public static void SetConnectionString(string connectionString)
        {
            if (ConnectionString == null)

            {
                ConnectionString = connectionString;
            }
            else
            {
                throw new Exception();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString, options => options.EnableRetryOnFailure());
            }
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.ReplaceService<IMigrationsIdGenerator, FixedMigrationsIdGenerator>();
        }        
    }
}
