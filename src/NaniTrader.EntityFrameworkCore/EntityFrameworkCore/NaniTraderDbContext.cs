using Microsoft.EntityFrameworkCore;
using NaniTrader.Brokers.Fyers;
using NaniTrader.Exchanges;
using NaniTrader.Exchanges.Securities.Equities;
using NaniTrader.Exchanges.Securities.Futures;
using NaniTrader.Exchanges.Securities.Options;
using System.Collections.Generic;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace NaniTrader.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ConnectionStringName("Default")]
public class NaniTraderDbContext : AbpDbContext<NaniTraderDbContext>, IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    public DbSet<FyersCredentials> FyersCredentials { get; set; }
    public DbSet<FyersSymbol> FyersSymbols { get; set; }
    public DbSet<Equity> Equities { get; set; }
    public DbSet<ETF> ETFs { get; set; }
    public DbSet<Index> Indexes { get; set; }
    public DbSet<EquityFuture> EquityFutures { get; set; }
    public DbSet<IndexFuture> IndexFutures { get; set; }
    public DbSet<EquityOption> EquityOptions { get; set; }
    public DbSet<IndexOption> IndexOptions { get; set; }
    public DbSet<Exchange> Exchanges { get; set; }

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext
     * and replaced it for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    #endregion

    public NaniTraderDbContext(DbContextOptions<NaniTraderDbContext> options)
        : base(options)
    {
        Database.SetCommandTimeout(600);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(NaniTraderConsts.DbTablePrefix + "YourEntities", NaniTraderConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<FyersCredentials>(b =>
        {
            b.ToTable(NaniTraderConsts.DbTablePrefix + "FyersCredentials", NaniTraderConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.UserId).IsRequired();
        });

        builder.Entity<FyersSymbol>(b =>
        {
            b.ToTable(NaniTraderConsts.DbTablePrefix + "FyersSymbols", NaniTraderConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.OwnsOne(x => x.PriceStep).Property(x => x.Amount).HasPrecision(20, 2);
            b.OwnsOne(x => x.StrikePrice).Property(x => x.Amount).HasPrecision(20, 2);
        });

        builder.Entity<Equity>(b =>
        {
            b.ToTable(NaniTraderConsts.DbTablePrefix + "Equities", NaniTraderConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.OwnsOne(x => x.PriceStep).Property(x => x.Amount).HasPrecision(20, 2);
        });

        builder.Entity<ETF>(b =>
        {
            b.ToTable(NaniTraderConsts.DbTablePrefix + "ETFs", NaniTraderConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.OwnsOne(x => x.PriceStep).Property(x => x.Amount).HasPrecision(20, 2);
        });

        builder.Entity<Index>(b =>
        {
            b.ToTable(NaniTraderConsts.DbTablePrefix + "Indexes", NaniTraderConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.OwnsOne(x => x.PriceStep).Property(x => x.Amount).HasPrecision(20, 2);
        });

        builder.Entity<EquityFuture>(b =>
        {
            b.ToTable(NaniTraderConsts.DbTablePrefix + "EquityFutures", NaniTraderConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.OwnsOne(x => x.PriceStep).Property(x => x.Amount).HasPrecision(20, 2);
        });

        builder.Entity<IndexFuture>(b =>
        {
            b.ToTable(NaniTraderConsts.DbTablePrefix + "IndexFutures", NaniTraderConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.OwnsOne(x => x.PriceStep).Property(x => x.Amount).HasPrecision(20, 2);
        });

        builder.Entity<EquityOption>(b =>
        {
            b.ToTable(NaniTraderConsts.DbTablePrefix + "EquityOptions", NaniTraderConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.OwnsOne(x => x.PriceStep).Property(x => x.Amount).HasPrecision(20, 2);
            b.OwnsOne(x => x.StrikePrice).Property(x => x.Amount).HasPrecision(20, 2);
        });

        builder.Entity<IndexOption>(b =>
        {
            b.ToTable(NaniTraderConsts.DbTablePrefix + "IndexOptions", NaniTraderConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.OwnsOne(x => x.PriceStep).Property(x => x.Amount).HasPrecision(20, 2);
            b.OwnsOne(x => x.StrikePrice).Property(x => x.Amount).HasPrecision(20, 2);
        });
    }
}
