using Volo.Abp.Modularity;

namespace ODY.Cihazkapinda
{
    [DependsOn(
        typeof(CihazkapindaApplicationModule),
        typeof(CihazkapindaDomainTestModule)
        )]
    public class CihazkapindaApplicationTestModule : AbpModule
    {

    }
}