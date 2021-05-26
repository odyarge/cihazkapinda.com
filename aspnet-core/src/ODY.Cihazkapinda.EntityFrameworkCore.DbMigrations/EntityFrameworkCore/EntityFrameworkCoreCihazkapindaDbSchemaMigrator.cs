using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ODY.Cihazkapinda.Data;
using Volo.Abp.DependencyInjection;

namespace ODY.Cihazkapinda.EntityFrameworkCore
{
    public class EntityFrameworkCoreCihazkapindaDbSchemaMigrator
        : ICihazkapindaDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreCihazkapindaDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the CihazkapindaMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<CihazkapindaMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}