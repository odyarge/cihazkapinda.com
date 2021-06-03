using Microsoft.EntityFrameworkCore;
using ODY.Cihazkapinda.BannerImages;
using ODY.Cihazkapinda.BannerSettings;
using ODY.Cihazkapinda.Categories;
using ODY.Cihazkapinda.GeneralSettings;
using ODY.Cihazkapinda.Licenses;
using ODY.Cihazkapinda.OperatorSettings;
using ODY.Cihazkapinda.ProductManagement;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.ThemeSettings;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace ODY.Cihazkapinda.EntityFrameworkCore
{
    public static class CihazkapindaDbContextModelCreatingExtensions
    {
        public static void ConfigureCihazkapinda(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<GeneralSetting>(b =>
            {
                b.ToTable(CihazkapindaConsts.DbTablePrefix + "GeneralSetting", CihazkapindaConsts.DbSchema);
                b.ConfigureByConvention();

                b.Ignore(x => x.ExtraProperties);
                b.Property(x => x.Logo).IsRequired();
                b.Property(x => x.Title).IsRequired();
                b.Property(x => x.Description).IsRequired();
                b.Property(x => x.SiteTheme).IsRequired();
            });

            builder.Entity<SiteSetting>(b =>
            {
                b.ToTable(CihazkapindaConsts.DbTablePrefix + "SiteSetting", CihazkapindaConsts.DbSchema);
                b.ConfigureByConvention();

                b.Ignore(x => x.ExtraProperties);
                b.Property(x => x.SITE_OWNER).IsRequired();
                b.Property(x => x.SITE_NAME).IsRequired();
                b.Property(x => x.SITE_OPERATOR).IsRequired();
                b.Property(x => x.SITE_LICENSE).IsRequired();
                b.Property(x => x.SITE_LICENSE_FINISH_TIME).IsRequired();
                b.Property(x => x.SITE_ACTIVATED).IsRequired();
            });

            builder.Entity<ThemeSetting>(b =>
            {
                b.ToTable(CihazkapindaConsts.DbTablePrefix + "ThemeSetting", CihazkapindaConsts.DbSchema);
                b.ConfigureByConvention();

                b.Ignore(x => x.ExtraProperties);
                b.Property(x => x.THEME_NAME).IsRequired();
                b.Property(x => x.THEME_PATH).IsRequired();
                b.Property(x => x.THEME_ACTIVATED).IsRequired();
            });

            builder.Entity<BannerSetting>(b =>
            {
                b.ToTable(CihazkapindaConsts.DbTablePrefix + "BannerSetting", CihazkapindaConsts.DbSchema);
                b.ConfigureByConvention();

                b.Ignore(x => x.ExtraProperties);
                b.Property(x => x.Title).IsRequired();

                b.HasMany(x => x.Images)
                    .WithOne(x => x.BannerSetting)
                    .HasForeignKey(x => x.BannerSettingId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            builder.Entity<BannerImage>(b =>
            {
                b.ToTable(CihazkapindaConsts.DbTablePrefix + "BannerImage", CihazkapindaConsts.DbSchema);
                b.ConfigureByConvention();

                b.Ignore(x => x.ExtraProperties);
                b.Property(x => x.Image).IsRequired();
                b.Property(x => x.Title).IsRequired();
                b.Property(x => x.Content).IsRequired();
            });

            builder.Entity<OperatorSetting>(b =>
            {
                b.ToTable(CihazkapindaConsts.DbTablePrefix + "OperatorSetting", CihazkapindaConsts.DbSchema);
                b.ConfigureByConvention();

                b.Ignore(x => x.ExtraProperties);
                b.Property(x => x.OperatorName).IsRequired();
                b.Property(x => x.Image).IsRequired();
            });

            builder.Entity<License>(b =>
            {
                b.ToTable(CihazkapindaConsts.DbTablePrefix + "License", CihazkapindaConsts.DbSchema);
                b.ConfigureByConvention();

                b.Ignore(x => x.ExtraProperties);
                b.Property(x => x.LICENSE_OWNER).IsRequired();
                b.Property(x => x.LICENSE).IsRequired();
            });

            builder.Entity<Product>(b =>
            {
                b.ToTable(CihazkapindaConsts.DbTablePrefix + "Product", CihazkapindaConsts.DbSchema);
                b.ConfigureByConvention();

                b.Ignore(x => x.ExtraProperties);
                b.Property(x => x.Title).IsRequired();
                b.Property(x => x.Description).IsRequired();
                b.Property(x => x.Price).HasPrecision(18,2);

                b.HasMany(x => x.Images)
                    .WithOne(x => x.Product)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne(x => x.ProductProperty)
                    .WithOne(x => x.Product)
                    .HasForeignKey<ProductProperty>(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ProductImage>(b =>
            {
                b.ToTable(CihazkapindaConsts.DbTablePrefix + "ProductImage", CihazkapindaConsts.DbSchema);
                b.ConfigureByConvention();

                b.Ignore(x => x.ExtraProperties);
                b.Property(x => x.Image).IsRequired();
            });

            builder.Entity<ProductProperty>(b =>
            {
                b.ToTable(CihazkapindaConsts.DbTablePrefix + "ProductProperty", CihazkapindaConsts.DbSchema);
                b.ConfigureByConvention();

                b.Ignore(x => x.ExtraProperties);
                b.Property(x => x.KEY).IsRequired();
                b.Property(x => x.VALUE).IsRequired();
                b.Property(x => x.TITLE).IsRequired();
            });

            builder.Entity<ProductPropertyTitle>(b =>
            {
                b.ToTable(CihazkapindaConsts.DbTablePrefix + "ProductPropertyTitle", CihazkapindaConsts.DbSchema);
                b.ConfigureByConvention();

                b.Ignore(x => x.ExtraProperties);
                b.Property(x => x.Name).IsRequired();
            });

            builder.Entity<Category>(b =>
            {
                b.ToTable(CihazkapindaConsts.DbTablePrefix + "Category", CihazkapindaConsts.DbSchema);
                b.ConfigureByConvention();

                b.Ignore(x => x.ExtraProperties);
                b.Property(x => x.Name).IsRequired();

                b.HasMany(x => x.Products)
                    .WithOne(x => x.Category)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
        }
    }
}