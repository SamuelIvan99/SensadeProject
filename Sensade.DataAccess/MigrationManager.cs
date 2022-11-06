using DbUp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Sensade.DataAccess;

public static class MigrationManager
{
    public static IHost Migrate<TContext>(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var configuration = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();

            logger.LogInformation("Start executing migrations for PostgreSQL database.");

            string connection = configuration.GetConnectionString(ConnectionString.Value);

            EnsureDatabase.For.PostgresqlDatabase(connection);

            var upgrader = DeployChanges.To
                .PostgresqlDatabase(connection)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .WithVariablesDisabled()
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                logger.LogError(result.Error, "An error occurred while executing migrations...");
                return host;
            }

            logger.LogInformation("Completed migrations for PostgreSQL database.");
        }

        return host;
    }
}
