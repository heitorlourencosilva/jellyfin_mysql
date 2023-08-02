using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Jellyfin.Server.Implementations.Migrations
{
    /// <summary>
    /// The design time factory for <see cref="JellyfinDbContext"/>.
    /// This is only used for the creation of migrations and not during runtime.
    /// </summary>
    internal class DesignTimeJellyfinDbFactory : IDesignTimeDbContextFactory<JellyfinDbContext>
    {
        public JellyfinDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JellyfinDbContext>();
            // var connectionString = Environment.GetEnvironmentVariable("JELLYFIN_CONN_STR") ?? "server=localhost;user=root;password=example;database=jellyfin";
            // var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));
            // optionsBuilder.UseMySql(connectionString, serverVersion, o => o.SchemaBehavior(MySqlSchemaBehavior.Ignore));

            var connectionString = Environment.GetEnvironmentVariable("JELLYFIN_CONN_STR") ?? "server=localhost;user=root;password=jellyfin;database=jellyfin";
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            optionsBuilder.UseMySql(connectionString, serverVersion, o => o.SchemaBehavior(MySqlSchemaBehavior.Ignore))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            return new JellyfinDbContext(optionsBuilder.Options);
        }
    }
}
