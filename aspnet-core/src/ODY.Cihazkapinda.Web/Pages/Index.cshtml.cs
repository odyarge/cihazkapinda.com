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
        public IndexModel(ISiteSettingAppService _siteSettingAppService,
            IGeneralSettingAppService generalSettingAppService) : base(_siteSettingAppService)
        {
            _generalSettingAppService = generalSettingAppService;
        }

        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckToActivatedAndInstalled();
            theme = await _generalSettingAppService.GetAsyncTheme(CurrentTenant.Id);
            return await Task.FromResult<IActionResult>(Page());
        }
    }
}