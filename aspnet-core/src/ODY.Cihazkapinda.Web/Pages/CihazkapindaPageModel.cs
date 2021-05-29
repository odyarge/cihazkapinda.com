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
                bool activated = checkActivated.SITE_ACTIVATED;
                if (activated == false)
                {
                    Response.Redirect("/Error/Activate/");
                }
            }
        }

        public async Task CheckAll()
        {
            await CheckToActivated();
            if (CurrentUser.IsAuthenticated == false)
            {
                Response.Redirect("/Error/404");
            }
        }
    }
}