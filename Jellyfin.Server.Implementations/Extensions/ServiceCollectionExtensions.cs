using System;
using EFCoreSecondLevelCacheInterceptor;
using MediaBrowser.Common.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Jellyfin.Server.Implementations.Extensions;

/// <summary>
/// Extensions for the <see cref="IServiceCollection"/> interface.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the <see cref="IDbContextFactory{TContext}"/> interface to the service collection with second level caching enabled.
    /// </summary>
    /// <param name="serviceCollection">An instance of the <see cref="IServiceCollection"/> interface.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddJellyfinDbContext(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddEFSecondLevelCache(options =>
            options.UseMemoryCacheProvider()
                .CacheAllQueries(CacheExpirationMode.Sliding, TimeSpan.FromMinutes(10))
                .DisableLogging(true)
                .UseCacheKeyPrefix("EF_")
                // Don't cache null values. Remove this optional setting if it's not necessary.
                .SkipCachingResults(result =>
                    result.Value is null || (result.Value is EFTableRows rows && rows.RowsCount == 0)));

        serviceCollection.AddPooledDbContextFactory<JellyfinDbContext>((serviceProvider, opt) =>
        {
            var applicationPaths = serviceProvider.GetRequiredService<IApplicationPaths>();
            // var connectionString = Environment.GetEnvironmentVariable("JELLYFIN_CONN_STR") ?? "server=localhost;user=root;password=example;database=jellyfin";
            // var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));
            // opt.UseMySql(connectionString, serverVersion, o => o.SchemaBehavior(MySqlSchemaBehavior.Ignore))
            //     .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>());

            var connectionString = Environment.GetEnvironmentVariable("JELLYFIN_CONN_STR") ?? "server=localhost;user=root;password=jellyfin;database=jellyfin";
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            opt.UseMySql(connectionString, serverVersion, o => o.SchemaBehavior(MySqlSchemaBehavior.Ignore))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>());
        });

        return serviceCollection;
    }
}
