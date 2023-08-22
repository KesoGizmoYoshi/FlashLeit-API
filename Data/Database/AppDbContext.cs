using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using flashleit_class_library.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashLeit_API.Data.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<SummaryModel> Summaries { get; set; }
    public DbSet<TestModel> TestTable { get; set; }
    public DbSet<CardModel> Cards { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<CollectionModel> Collections { get; set; }
    public DbSet<UserStatsModel> UserStats { get; set; }
    public DbSet<CounterModel> Counters { get; set; }
    public DbSet<AchievementModel> Achievements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetConnectionStringFromAzureKeyVault());
        }
    }

    private string GetConnectionStringFromAzureKeyVault()
    {
        SecretClientOptions options = new SecretClientOptions()
        {
            Retry =
            {
                Delay= TimeSpan.FromSeconds(2),
                MaxDelay = TimeSpan.FromSeconds(16),
                MaxRetries = 5,
                Mode = RetryMode.Exponential
            }
        };

        var client = new SecretClient(new Uri("https://flashleit-keys.vault.azure.net/"), new DefaultAzureCredential(), options);

        KeyVaultSecret secret = client.GetSecret("ConnectionString--FlashleitDbConnection");

        return secret.Value;
    }
}

