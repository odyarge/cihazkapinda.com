using ODY.Cihazkapinda.Localization;
using ODY.Cihazkapinda.SiteSettings;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ODY.Cihazkapinda.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class CihazkapindaPageModel : AbpPageModel
    {
        private readonly ISiteSettingAppService _siteSettingAppService;

        protected CihazkapindaPageModel(ISiteSettingAppService siteSettingAppService)
        {
            LocalizationResourceType = typeof(CihazkapindaResource);
            _siteSettingAppService = siteSettingAppService;
        }

        public async Task CheckToActivated()
        {
            if (CurrentTenant.Id != null)
            {
                var checkActivated = await _siteSettingAppService.GetAsyncByTenantName(CurrentTenant.Name);
                if (checkActivated != null)
                {
                    bool activated = checkActivated.SITE_ACTIVATED;
                    if (activated == false)
                    {
                        Response.Redirect("/Error/Activate/");
                    }
                }
                else
                {
                    Response.Redirect("/Error/404");
                }
            }
        }

        public async Task CheckToInstalled()
        {
            if (CurrentTenant.Id != null)
            {
                var checkActivated = await _siteSettingAppService.GetAsyncByTenantName(CurrentTenant.Name);
                if (checkActivated != null)
                {
                    bool activated = checkActivated.SITE_INSTALL;
                    if (activated == false)
                    {
                        Response.Redirect("/Install/Index");
                    }
                }
                else
                {
                    Response.Redirect("/Error/404");
                }
            }
        }

        public async Task CheckToActivatedAndInstalled()
        {
            if (CurrentTenant.Id != null)
            {
                var checkActivated = await _siteSettingAppService.GetAsyncByTenantName(CurrentTenant.Name);
                if (checkActivated != null)
                {
                    bool activated = checkActivated.SITE_ACTIVATED;
                    bool installed = checkActivated.SITE_INSTALL;
                    if (installed == false)
                    {
                        Response.Redirect("/Install/Index");
                    }
                    if (activated == false)
                    {
                        Response.Redirect("/Error/Activate/");
                    }
                }
                else
                {
                    Response.Redirect("/Error/404");
                }
            }
        }

        public async Task CheckAll()
        {
            if (CurrentUser.IsAuthenticated == false)
            {
                Response.Redirect("/Account/Login");
            }
            await CheckToActivatedAndInstalled();
        }
    }
}