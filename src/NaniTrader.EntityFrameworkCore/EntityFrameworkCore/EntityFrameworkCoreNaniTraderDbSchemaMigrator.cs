using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NaniTrader.Data;
using Volo.Abp.DependencyInjection;

namespace NaniTrader.EntityFrameworkCore;

public class EntityFrameworkCoreNaniTraderDbSchemaMigrator
    : INaniTraderDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreNaniTraderDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        await _serviceProvider
            .GetRequiredService<NaniTraderDbContext>()
            .Database
            .MigrateAsync();
    }
}
