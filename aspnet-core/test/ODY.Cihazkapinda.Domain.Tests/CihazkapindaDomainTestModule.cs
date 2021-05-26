using ODY.Cihazkapinda.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ODY.Cihazkapinda
{
    [DependsOn(
        typeof(CihazkapindaEntityFrameworkCoreTestModule)
        )]
    public class CihazkapindaDomainTestModule : AbpModule
    {

    }
}