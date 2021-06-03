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

            var SiteSettingsGroup = context.AddGroup(CihazkapindaPermissions.SiteSettingsGroup, L("Permission:SiteSettings"), Volo.Abp.MultiTenancy.MultiTenancySides.Host);
            SiteSettingsGroup.MultiTenancySide = Volo.Abp.MultiTenancy.MultiTenancySides.Host;
            var GeneralSettingsGroup = context.AddGroup(CihazkapindaPermissions.GeneralSettingsGroup, L("Permission:GeneralSettings"));
            var ThemeSettingsGroup = context.AddGroup(CihazkapindaPermissions.ThemeSettingsGroup, L("Permission:ThemeSettings"), Volo.Abp.MultiTenancy.MultiTenancySides.Host);
            var BannerSettingsGroup = context.AddGroup(CihazkapindaPermissions.BannerSettingsGroup, L("Permission:Banner"));
            var CategoriesGroup = context.AddGroup(CihazkapindaPermissions.CategoriesGroup, L("Permission:Categories"));
            var ProductManagementGroup = context.AddGroup(CihazkapindaPermissions.ProductManagementGroup, L("Permission:ProductManagement"));
            var OperatorSettingsGroup = context.AddGroup(CihazkapindaPermissions.OperatorSettingsGroup, L("Permission:OperatorSettings"), Volo.Abp.MultiTenancy.MultiTenancySides.Host);
            var LicensesGroup = context.AddGroup(CihazkapindaPermissions.LicensesGroup, L("Permission:Licenses"), Volo.Abp.MultiTenancy.MultiTenancySides.Host);

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

            var operatorSettingGroup = OperatorSettingsGroup.AddPermission(CihazkapindaPermissions.OperatorSettings.OperatorSettingDefault, L("Permission:OperatorSettings"));
            operatorSettingGroup.AddChild(CihazkapindaPermissions.OperatorSettings.MenuList, L("Permission:OperatorSettingsMenuList"));
            operatorSettingGroup.AddChild(CihazkapindaPermissions.OperatorSettings.List, L("Permission:OperatorSettingsList"));
            operatorSettingGroup.AddChild(CihazkapindaPermissions.OperatorSettings.Create, L("Permission:OperatorSettingsCreate"));
            operatorSettingGroup.AddChild(CihazkapindaPermissions.OperatorSettings.Edit, L("Permission:OperatorSettingsEdit"));
            operatorSettingGroup.AddChild(CihazkapindaPermissions.OperatorSettings.Delete, L("Permission:OperatorSettingsDelete"));

            var licensesSettingGroup = LicensesGroup.AddPermission(CihazkapindaPermissions.Licenses.LicenseDefault, L("Permission:Licenses"));
            licensesSettingGroup.AddChild(CihazkapindaPermissions.Licenses.MenuList, L("Permission:LicensesMenuList"));
            licensesSettingGroup.AddChild(CihazkapindaPermissions.Licenses.List, L("Permission:LicensesList"));
            licensesSettingGroup.AddChild(CihazkapindaPermissions.Licenses.Create, L("Permission:LicensesCreate"));
            licensesSettingGroup.AddChild(CihazkapindaPermissions.Licenses.Edit, L("Permission:LicensesEdit"));
            licensesSettingGroup.AddChild(CihazkapindaPermissions.Licenses.Delete, L("Permission:LicensesDelete"));

            var categoriesGroup = CategoriesGroup.AddPermission(CihazkapindaPermissions.Categories.CategoriesDefault, L("Permission:Categories"));
            categoriesGroup.AddChild(CihazkapindaPermissions.Categories.MenuList, L("Permission:CategoriesMenuList"));
            categoriesGroup.AddChild(CihazkapindaPermissions.Categories.List, L("Permission:CategoriesList"));
            categoriesGroup.AddChild(CihazkapindaPermissions.Categories.Create, L("Permission:CategoriesCreate"));
            categoriesGroup.AddChild(CihazkapindaPermissions.Categories.Edit, L("Permission:CategoriesEdit"));
            categoriesGroup.AddChild(CihazkapindaPermissions.Categories.Delete, L("Permission:CategoriesDelete"));

            var productsGroup = ProductManagementGroup.AddPermission(CihazkapindaPermissions.Products.ProductsDefault, L("Permission:Products"));
            productsGroup.AddChild(CihazkapindaPermissions.Products.MenuList, L("Permission:ProductsMenuList"));
            productsGroup.AddChild(CihazkapindaPermissions.Products.List, L("Permission:ProductsList"));
            productsGroup.AddChild(CihazkapindaPermissions.Products.Create, L("Permission:ProductsCreate"));
            productsGroup.AddChild(CihazkapindaPermissions.Products.Edit, L("Permission:ProductsEdit"));
            productsGroup.AddChild(CihazkapindaPermissions.Products.Delete, L("Permission:ProductsDelete"));

            var productImagesGroup = ProductManagementGroup.AddPermission(CihazkapindaPermissions.ProductImages.ProductImagesDefault, L("Permission:ProductImages"));
            productImagesGroup.AddChild(CihazkapindaPermissions.ProductImages.MenuList, L("Permission:ProductImagesMenuList"));
            productImagesGroup.AddChild(CihazkapindaPermissions.ProductImages.List, L("Permission:ProductImagesList"));
            productImagesGroup.AddChild(CihazkapindaPermissions.ProductImages.Create, L("Permission:ProductImagesCreate"));
            productImagesGroup.AddChild(CihazkapindaPermissions.ProductImages.Edit, L("Permission:ProductImagesEdit"));
            productImagesGroup.AddChild(CihazkapindaPermissions.ProductImages.Delete, L("Permission:ProductImagesDelete"));

            var productPropertiesGroup = ProductManagementGroup.AddPermission(CihazkapindaPermissions.ProductProperties.ProductPropertiesDefault, L("Permission:ProductProperties"));
            productPropertiesGroup.AddChild(CihazkapindaPermissions.ProductProperties.MenuList, L("Permission:ProductPropertiesMenuList"));
            productPropertiesGroup.AddChild(CihazkapindaPermissions.ProductProperties.List, L("Permission:ProductPropertiesList"));
            productPropertiesGroup.AddChild(CihazkapindaPermissions.ProductProperties.Create, L("Permission:ProductPropertiesCreate"));
            productPropertiesGroup.AddChild(CihazkapindaPermissions.ProductProperties.Edit, L("Permission:ProductPropertiesEdit"));
            productPropertiesGroup.AddChild(CihazkapindaPermissions.ProductProperties.Delete, L("Permission:ProductPropertiesDelete"));

            var productPropertyTitlesGroup = ProductManagementGroup.AddPermission(CihazkapindaPermissions.ProductPropertyTitles.ProductPropertyTitlesDefault, L("Permission:ProductPropertyTitles"));
            productPropertyTitlesGroup.AddChild(CihazkapindaPermissions.ProductPropertyTitles.MenuList, L("Permission:ProductPropertyTitlesMenuList"));
            productPropertyTitlesGroup.AddChild(CihazkapindaPermissions.ProductPropertyTitles.List, L("Permission:ProductPropertyTitlesList"));
            productPropertyTitlesGroup.AddChild(CihazkapindaPermissions.ProductPropertyTitles.Create, L("Permission:ProductPropertyTitlesCreate"));
            productPropertyTitlesGroup.AddChild(CihazkapindaPermissions.ProductPropertyTitles.Edit, L("Permission:ProductPropertyTitlesEdit"));
            productPropertyTitlesGroup.AddChild(CihazkapindaPermissions.ProductPropertyTitles.Delete, L("Permission:ProductPropertyTitlesDelete"));

            //Define your own permissions here. Example:
            //myGroup.AddPermission(CihazkapindaPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CihazkapindaResource>(name);
        }
    }
}
