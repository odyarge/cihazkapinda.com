using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace ODY.Cihazkapinda.EntityFrameworkCore
{
    public static class CihazkapindaDbContextModelCreatingExtensions
    {
        public static void ConfigureCihazkapinda(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(CihazkapindaConsts.DbTablePrefix + "YourEntities", CihazkapindaConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}