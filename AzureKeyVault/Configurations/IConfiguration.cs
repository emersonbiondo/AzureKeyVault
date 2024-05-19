using Microsoft.EntityFrameworkCore;

namespace AzureKeyVault.Configurations
{
    public interface IConfiguration
    {
        public void ModelCreating(ModelBuilder modelBuilder);
    }
}
