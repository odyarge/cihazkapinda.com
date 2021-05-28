using Microsoft.EntityFrameworkCore;
using ODY.Cihazkapinda.BannerImages;
using ODY.Cihazkapinda.BannerSettings;
using ODY.Cihazkapinda.GeneralSettings;
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
                b.Property(x => x.WelcomeMessage).IsRequired();

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
            });
        }
    }
}