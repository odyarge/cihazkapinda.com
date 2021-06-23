using Microsoft.EntityFrameworkCore;
using ODY.Cihazkapinda.BannerImages;
using ODY.Cihazkapinda.BannerSettings;
using ODY.Cihazkapinda.Categories;
using ODY.Cihazkapinda.Customers;
using ODY.Cihazkapinda.GeneralSettings;
using ODY.Cihazkapinda.Licenses;
using ODY.Cihazkapinda.MessageData;
using ODY.Cihazkapinda.OperatorSettings;
using ODY.Cihazkapinda.ProductManagement;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.ThemeSettings;
using ODY.Cihazkapinda.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace ODY.Cihazkapinda.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See CihazkapindaMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class CihazkapindaDbContext : AbpDbContext<CihazkapindaDbContext>
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<License> Licenses { get; set; }

        #region IMPORTANT_SETTINGS
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<ThemeSetting> ThemeSettings { get; set; }
        public DbSet<OperatorSetting> OperatorSettings { get; set; }
        #endregion IMPORTANT_SETTINGS

        #region GENERAL_SETTINGS
        public DbSet<GeneralSetting> GeneralSettings { get; set; }
        #endregion GENERAL_SETTINGS

        #region FRONTEND_SETTINGS
        public DbSet<BannerSetting> BannerSettings { get; set; }
        public DbSet<BannerImage> BannerImages { get; set; }
        #endregion FRONTEND_SETTINGS

        #region CATEGORY
        public DbSet<Category> Category { get; set; }
        #endregion CATEGORY

        #region PRODUCTS
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<ProductProperty> ProductProperty { get; set; }
        public DbSet<ProductPropertyTitle> ProductPropertyTitle { get; set; }
        public DbSet<ProductPropertyTemplate> ProductPropertyTemplate { get; set; }
        public DbSet<ProductPropertySubTemplate> ProductPropertySubTemplate { get; set; }
        public DbSet<ProductInfo> ProductInfo { get; set; }
        public DbSet<ProductInfoTemplate> ProductInfoTemplate { get; set; }
        #endregion PRODUCTS

        #region MESSAGE
        public DbSet<Messages> Messages { get; set; }
        #endregion MESSAGE

        #region CUSTOMER
        public DbSet<Customer> Customer { get; set; }
        #endregion CUSTOMER

        public CihazkapindaDbContext(DbContextOptions<CihazkapindaDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser
                
                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the CihazkapindaEfCoreEntityExtensionMappings class
                 */
            });

            /* Configure your own tables/entities inside the ConfigureCihazkapinda method */

            builder.ConfigureCihazkapinda();
        }
    }
}
