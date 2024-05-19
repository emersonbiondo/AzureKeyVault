using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AzureKeyVault.Context;
using AzureKeyVault.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT_DEVELOPMENT");

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var configuration = configurationBuilder.Build();

var connections = configuration.GetSection(nameof(Connections)).Get<Connections>();

Console.WriteLine("appsettings values:");
Console.WriteLine($"Connections = {connections}");
Console.WriteLine(string.Empty);

if (connections != null)
{
    var connectionString = string.Empty;

    if (connections.AzureKeyVault)
    {
        SecretClientOptions options = new SecretClientOptions()
        {
            Retry = {
                Delay= TimeSpan.FromSeconds(connections.Delay),
                MaxDelay = TimeSpan.FromSeconds(connections.MaxDelay),
                MaxRetries = connections.MaxRetries,
                Mode = RetryMode.Exponential
            }
        };

        SecretClient secretClient;

        if (connections.DefaultCredential)
        {
            secretClient = new SecretClient(new Uri(connections.Endpoint), new DefaultAzureCredential(), options);
        }
        else
        {
            var credential = new UsernamePasswordCredential(connections.AzureUsername, connections.AzurePassword, connections.TenantId, connections.ClientId);

            secretClient = new SecretClient(new Uri(connections.Endpoint), credential, options);
        }

        var keyVaultSecret = secretClient.GetSecret(connections.SecretName);

        connectionString = keyVaultSecret.Value.Value;

        Console.WriteLine("result from keyvault:");
        Console.WriteLine($"{connections.SecretName} = {connectionString}");
        Console.WriteLine(string.Empty);
    }
    else
    {
        connectionString = connections.DefaultConnectionDB;
    }

    Console.WriteLine("connectionString values:");
    Console.WriteLine($"connectionString = {connectionString}");
    Console.WriteLine(string.Empty);

    if (connections.TestMigration)
    {
        MyDBContext.SetConnectionString(connectionString);

        using (var context = new MyDBContext())
        {
            context.Database.Migrate();
            Console.WriteLine($"Migrate Sucess!");
            Console.WriteLine(string.Empty);

            var samples = context.Set<Sample>().ToList();

            Console.WriteLine($"SELECT test:");
            foreach (var sample in samples)
            {
                Console.WriteLine(sample.ToString());
            }
        }
    }
}

Console.WriteLine(string.Empty);
Console.WriteLine($"Done.");