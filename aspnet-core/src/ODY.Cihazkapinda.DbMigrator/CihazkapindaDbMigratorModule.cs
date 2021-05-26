using ODY.Cihazkapinda.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace ODY.Cihazkapinda.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(CihazkapindaEntityFrameworkCoreDbMigrationsModule),
        typeof(CihazkapindaApplicationContractsModule)
        )]
    public class CihazkapindaDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
