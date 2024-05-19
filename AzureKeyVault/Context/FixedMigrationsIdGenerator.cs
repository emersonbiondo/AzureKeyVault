using Microsoft.EntityFrameworkCore.Migrations;

namespace AzureKeyVault.Context
{
    public class FixedMigrationsIdGenerator : IMigrationsIdGenerator
    {
        public virtual string GetName(string id)
            => id[..];
        public virtual bool IsValidId(string value)
            => true;
        public virtual string GenerateId(string name)
        {
            return name;
        }
    }
}
