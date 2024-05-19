using AzureKeyVault.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureKeyVault.Configurations
{
    public class SampleConfiguration : IConfiguration
    {
        public void ModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sample>(entity =>
            {
            });

            modelBuilder.Entity<Sample>().HasData(
                new Sample()
                {
                    ID = 1,
                    Name = "Test",
                    Description = "Test",
                }
            );
        }
    }
}
