using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ODY.Cihazkapinda.Data
{
    /* This is used if database provider does't define
     * ICihazkapindaDbSchemaMigrator implementation.
     */
    public class NullCihazkapindaDbSchemaMigrator : ICihazkapindaDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}