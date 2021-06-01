using Microsoft.AspNetCore.Mvc;
using ODY.Cihazkapinda.GeneralSettings;
using ODY.Cihazkapinda.SiteSettings;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Pages
{
    public class IndexModel : CihazkapindaPageModel
    {
        public string theme { get; set; }

        private readonly IGeneralSettingAppService _generalSettingAppService;
        private readonly ISiteSettingAppService _siteSettingAppService;
        public IndexModel(ISiteSettingAppService siteSettingAppService,
            IGeneralSettingAppService generalSettingAppService) : base(siteSettingAppService)
        {
            _generalSettingAppService = generalSettingAppService;
            _siteSettingAppService = siteSettingAppService;
        }

        public async virtual Task<IActionResult> OnGetAsync()
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
                        return RedirectSafely("/Install/Index");
                    }
                    if (activated == false)
                    {
                        return RedirectSafely("/Error/Activate/");
                    }
                }
                else
                {
                    return RedirectSafely("/Error/404");
                }
            }

            theme = await _generalSettingAppService.GetAsyncTheme(CurrentTenant.Id);
            return await Task.FromResult<IActionResult>(Page());
        }
    }
}