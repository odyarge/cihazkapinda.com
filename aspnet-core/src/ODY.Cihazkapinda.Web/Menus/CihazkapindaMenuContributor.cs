using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using ODY.Cihazkapinda.Localization;
using ODY.Cihazkapinda.MultiTenancy;
using ODY.Cihazkapinda.Permissions;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace ODY.Cihazkapinda.Web.Menus
{
    public class CihazkapindaMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            if (!MultiTenancyConsts.IsEnabled)
            {
                var administration = context.Menu.GetAdministration();
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            }

            var l = context.GetLocalizer<CihazkapindaResource>();

            context.Menu.Items.RemoveAt(0);
            context.Menu.Items.Insert(0, new ApplicationMenuItem(CihazkapindaMenus.Home, l["Menu:Home"], "/Admin/", "fa fa-home"));

            #region COMPONENTS
            var componentsMenu = new ApplicationMenuItem(CihazkapindaMenus.Components, l["Menu:Components"], "", "fa fa-cube");
            context.Menu.Items.Insert(1, componentsMenu);
            #region COMPONENTS_SUB_MENUS
            if (await context.IsGrantedAsync(CihazkapindaPermissions.BannerSettings.MenuList))
            {
                componentsMenu.AddItem(new ApplicationMenuItem(CihazkapindaMenus.BannerSettings, l["Menu:BannerSettings"], "/Admin/BannerSettings/"));
            }
            if (await context.IsGrantedAsync(CihazkapindaPermissions.BannerImages.MenuList))
            {
                componentsMenu.AddItem(new ApplicationMenuItem(CihazkapindaMenus.BannerImages, l["Menu:BannerImages"], "/Admin/BannerImages/"));
            }
            if (await context.IsGrantedAsync(CihazkapindaPermissions.Categories.MenuList))
            {
                componentsMenu.AddItem(new ApplicationMenuItem(CihazkapindaMenus.Categories, l["Menu:Categories"], "/Admin/Categories/"));
            }
            if (await context.IsGrantedAsync(CihazkapindaPermissions.Products.MenuList))
            {
                var productManagement = new ApplicationMenuItem(CihazkapindaMenus.ProductManagement, l["Menu:ProductManagement"], "");

                if (await context.IsGrantedAsync(CihazkapindaPermissions.Products.MenuList))
                {
                    productManagement.AddItem(new ApplicationMenuItem(CihazkapindaMenus.Products, l["Menu:Products"], "/Admin/ProductManagement/Products/"));
                }

                if (await context.IsGrantedAsync(CihazkapindaPermissions.Products.Create))
                {
                    productManagement.AddItem(new ApplicationMenuItem(CihazkapindaMenus.ProductAdd, l["Menu:ProductAdd"], "/Admin/ProductManagement/ProductAdd/"));
                }

                if (await context.IsGrantedAsync(CihazkapindaPermissions.ProductImages.MenuList))
                {
                    productManagement.AddItem(new ApplicationMenuItem(CihazkapindaMenus.ProductImages, l["Menu:ProductImages"], "/Admin/ProductManagement/ProductImages/"));
                }

                if (await context.IsGrantedAsync(CihazkapindaPermissions.ProductInfoTemplate.MenuList))
                {
                    productManagement.AddItem(new ApplicationMenuItem(CihazkapindaMenus.ProductInfoTemplate, l["Menu:ProductInfoTemplate"], "/Admin/ProductManagement/ProductInfoTemplate/"));
                }


                if (await context.IsGrantedAsync(CihazkapindaPermissions.ProductPropertyTitles.MenuList) && await context.IsGrantedAsync(TenantManagementPermissions.Tenants.Default))
                {
                    var productProperties = new ApplicationMenuItem(CihazkapindaMenus.ProductProperties, l["Menu:ProductProperties"], "");

                    if (await context.IsGrantedAsync(CihazkapindaPermissions.ProductPropertyTitles.MenuList) && await context.IsGrantedAsync(TenantManagementPermissions.Tenants.Default))
                    {
                        productProperties.AddItem(new ApplicationMenuItem(CihazkapindaMenus.ProductPropertyTitle, l["Menu:ProductPropertyTitle"], "/Admin/ProductManagement/ProductPropertyTitles/"));
                    }

                    if (await context.IsGrantedAsync(CihazkapindaPermissions.ProductPropertyTemplate.MenuList) && await context.IsGrantedAsync(TenantManagementPermissions.Tenants.Default))
                    {
                        productProperties.AddItem(new ApplicationMenuItem(CihazkapindaMenus.ProductPropertyTemplate, l["Menu:ProductPropertyTemplate"], "/Admin/ProductManagement/ProductPropertyTemplate/"));
                    }

                    if (await context.IsGrantedAsync(CihazkapindaPermissions.ProductPropertySubTemplate.MenuList) && await context.IsGrantedAsync(TenantManagementPermissions.Tenants.Default))
                    {
                        productProperties.AddItem(new ApplicationMenuItem(CihazkapindaMenus.ProductPropertySubTemplate, l["Menu:ProductPropertySubTemplate"], "/Admin/ProductManagement/ProductPropertySubTemplate/"));
                    }
                    productManagement.AddItem(productProperties);
                }

                componentsMenu.AddItem(productManagement);
            }
            #endregion COMPONENTS_SUB_MENUS
            #endregion COMPONENTS

            #region SETTINGS
            var settingsMenu = new ApplicationMenuItem(CihazkapindaMenus.Settings, l["Menu:Settings"], "", "fa fa-cog");
            context.Menu.GetAdministration().AddItem(settingsMenu);
            #region SETTINGS_SUB_MENUS
            if (await context.IsGrantedAsync(CihazkapindaPermissions.SiteSettings.MenuList) && await context.IsGrantedAsync(TenantManagementPermissions.Tenants.Default))
            {
                settingsMenu.AddItem(new ApplicationMenuItem(CihazkapindaMenus.SiteSettings, l["Menu:SiteSettings"], "/Admin/SiteSettings/"));
            }

            if (await context.IsGrantedAsync(CihazkapindaPermissions.GeneralSettings.MenuList))
            {
                settingsMenu.AddItem(new ApplicationMenuItem(CihazkapindaMenus.GeneralSettings, l["Menu:GeneralSettings"], "/Admin/GeneralSettings/"));
            }

            if (await context.IsGrantedAsync(CihazkapindaPermissions.ThemeSettings.MenuList) && await context.IsGrantedAsync(TenantManagementPermissions.Tenants.Default))
            {
                settingsMenu.AddItem(new ApplicationMenuItem(CihazkapindaMenus.ThemeSettings, l["Menu:ThemeSettings"], "/Admin/ThemeSettings/"));
            }

            if (await context.IsGrantedAsync(CihazkapindaPermissions.OperatorSettings.MenuList) && await context.IsGrantedAsync(TenantManagementPermissions.Tenants.Default))
            {
                settingsMenu.AddItem(new ApplicationMenuItem(CihazkapindaMenus.OperatorSettings, l["Menu:OperatorSettings"], "/Admin/OperatorSettings/"));
            }

            if (await context.IsGrantedAsync(CihazkapindaPermissions.Licenses.MenuList) && await context.IsGrantedAsync(TenantManagementPermissions.Tenants.Default))
            {
                settingsMenu.AddItem(new ApplicationMenuItem(CihazkapindaMenus.Licenses, l["Menu:Licenses"], "/Admin/Licenses/"));
            }
            #endregion SETTINGS_SUB_MENUS
            #endregion SETTINGS

        }
    }
}
