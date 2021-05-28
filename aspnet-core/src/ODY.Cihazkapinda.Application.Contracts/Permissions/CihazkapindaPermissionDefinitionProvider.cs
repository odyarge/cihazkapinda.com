using ODY.Cihazkapinda.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ODY.Cihazkapinda.Permissions
{
    public class CihazkapindaPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(CihazkapindaPermissions.GroupName);
            var SiteSettingsGroup = context.AddGroup(CihazkapindaPermissions.SiteSettingsGroup, L("Permission:SiteSettings"));
            var GeneralSettingsGroup = context.AddGroup(CihazkapindaPermissions.GeneralSettingsGroup, L("Permission:GeneralSettings"));
            var ThemeSettingsGroup = context.AddGroup(CihazkapindaPermissions.ThemeSettingsGroup, L("Permission:ThemeSettings"));
            var BannerSettingsGroup = context.AddGroup(CihazkapindaPermissions.BannerSettingsGroup, L("Permission:Banner"));

            var siteSettingGroup = SiteSettingsGroup.AddPermission(CihazkapindaPermissions.SiteSettings.SiteSettingDefault, L("Permission:SiteSettings"));
            siteSettingGroup.AddChild(CihazkapindaPermissions.SiteSettings.MenuList, L("Permission:SiteSettingsMenuList"));
            siteSettingGroup.AddChild(CihazkapindaPermissions.SiteSettings.List, L("Permission:SiteSettingsList"));
            siteSettingGroup.AddChild(CihazkapindaPermissions.SiteSettings.Create, L("Permission:SiteSettingsCreate"));
            siteSettingGroup.AddChild(CihazkapindaPermissions.SiteSettings.Edit, L("Permission:SiteSettingsEdit"));
            siteSettingGroup.AddChild(CihazkapindaPermissions.SiteSettings.Delete, L("Permission:SiteSettingsDelete"));

            var generalSettingGroup = GeneralSettingsGroup.AddPermission(CihazkapindaPermissions.GeneralSettings.GeneralSettingDefault, L("Permission:GeneralSettings"));
            generalSettingGroup.AddChild(CihazkapindaPermissions.GeneralSettings.MenuList, L("Permission:GeneralSettingsMenuList"));
            generalSettingGroup.AddChild(CihazkapindaPermissions.GeneralSettings.List, L("Permission:GeneralSettingsList"));
            generalSettingGroup.AddChild(CihazkapindaPermissions.GeneralSettings.Create, L("Permission:GeneralSettingsCreate"));
            generalSettingGroup.AddChild(CihazkapindaPermissions.GeneralSettings.Edit, L("Permission:GeneralSettingsEdit"));
            generalSettingGroup.AddChild(CihazkapindaPermissions.GeneralSettings.Delete, L("Permission:GeneralSettingsDelete"));

            var themeSettingGroup = ThemeSettingsGroup.AddPermission(CihazkapindaPermissions.ThemeSettings.ThemeSettingDefault, L("Permission:ThemeSettings"));
            themeSettingGroup.AddChild(CihazkapindaPermissions.ThemeSettings.MenuList, L("Permission:ThemeSettingsMenuList"));
            themeSettingGroup.AddChild(CihazkapindaPermissions.ThemeSettings.List, L("Permission:ThemeSettingsList"));
            themeSettingGroup.AddChild(CihazkapindaPermissions.ThemeSettings.Create, L("Permission:ThemeSettingsCreate"));
            themeSettingGroup.AddChild(CihazkapindaPermissions.ThemeSettings.Edit, L("Permission:ThemeSettingsEdit"));
            themeSettingGroup.AddChild(CihazkapindaPermissions.ThemeSettings.Delete, L("Permission:ThemeSettingsDelete"));

            var bannerSettingGroup = BannerSettingsGroup.AddPermission(CihazkapindaPermissions.BannerSettings.BannerSettingDefault, L("Permission:Banner"));
            bannerSettingGroup.AddChild(CihazkapindaPermissions.BannerSettings.MenuList, L("Permission:BannerMenuList"));
            bannerSettingGroup.AddChild(CihazkapindaPermissions.BannerSettings.List, L("Permission:BannerList"));
            bannerSettingGroup.AddChild(CihazkapindaPermissions.BannerSettings.Create, L("Permission:BannerCreate"));
            bannerSettingGroup.AddChild(CihazkapindaPermissions.BannerSettings.Edit, L("Permission:BannerEdit"));
            bannerSettingGroup.AddChild(CihazkapindaPermissions.BannerSettings.Delete, L("Permission:BannerDelete"));

            var bannerImageGroup = BannerSettingsGroup.AddPermission(CihazkapindaPermissions.BannerImages.BannerImageDefault, L("Permission:BannerImages"));
            bannerImageGroup.AddChild(CihazkapindaPermissions.BannerImages.MenuList, L("Permission:BannerImagesMenuList"));
            bannerImageGroup.AddChild(CihazkapindaPermissions.BannerImages.List, L("Permission:BannerImagesList"));
            bannerImageGroup.AddChild(CihazkapindaPermissions.BannerImages.Create, L("Permission:BannerImagesCreate"));
            bannerImageGroup.AddChild(CihazkapindaPermissions.BannerImages.Edit, L("Permission:BannerImagesEdit"));
            bannerImageGroup.AddChild(CihazkapindaPermissions.BannerImages.Delete, L("Permission:BannerImagesDelete"));

            //Define your own permissions here. Example:
            //myGroup.AddPermission(CihazkapindaPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CihazkapindaResource>(name);
        }
    }
}
