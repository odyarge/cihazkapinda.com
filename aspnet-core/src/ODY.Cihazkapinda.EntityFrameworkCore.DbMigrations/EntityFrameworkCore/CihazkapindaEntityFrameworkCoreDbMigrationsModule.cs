using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace ODY.Cihazkapinda.EntityFrameworkCore
{
    [DependsOn(
        typeof(CihazkapindaEntityFrameworkCoreModule)
        )]
    public class CihazkapindaEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<CihazkapindaMigrationsDbContext>();
        }
    }
}
