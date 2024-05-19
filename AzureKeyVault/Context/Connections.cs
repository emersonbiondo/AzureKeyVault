namespace AzureKeyVault.Context
{
    public class Connections
    {
        public bool AzureKeyVault { get; set; }
        public string DefaultConnectionDB { get; set; }
        public string Endpoint { get; set; }
        public string SecretName { get; set; }
        public bool DefaultCredential { get; set; }
        public int Delay { get; set; }
        public int MaxDelay { get; set; }
        public int MaxRetries { get; set; }
        public string AzureUsername { get; set; }
        public string AzurePassword { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public bool TestMigration { get; set; }

        public Connections()
        {
            AzureKeyVault = false;
            DefaultConnectionDB = string.Empty;
            Endpoint = string.Empty;
            SecretName = string.Empty;
            DefaultCredential = true;
            Delay = 1;
            MaxDelay = 1;
            MaxRetries = 1;
            AzureUsername = string.Empty;
            AzurePassword = string.Empty;
            TenantId = string.Empty;
            ClientId = string.Empty;
            TestMigration = false;
        }

        public override string ToString()
        {
            return $"AzureKeyVault: {AzureKeyVault}, " +
                $"DefaultConnectionDB: {DefaultConnectionDB}, " +
                $"Endpoint: {Endpoint}, " +
                $"SecretName: {SecretName}, " +
                $"DefaultCredential: {DefaultCredential}, " +
                $"Delay: {Delay}, " +
                $"MaxDelay: {MaxDelay}, " +
                $"MaxRetries: {MaxRetries}, " +
                $"AzureUsername: {AzureUsername}, " +
                $"AzurePassword: {AzurePassword}, " +
                $"TenantId: {TenantId}, " +
                $"ClientId: {ClientId}, " +
                $"TestMigration: {TestMigration}";
        }
    }
}
