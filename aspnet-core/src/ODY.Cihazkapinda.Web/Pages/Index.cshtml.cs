using Microsoft.AspNetCore.Mvc;
using ODY.Cihazkapinda.SiteSettings;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Pages
{
    public class IndexModel : CihazkapindaPageModel
    {
        public IndexModel(ISiteSettingAppService _siteSettingAppService) : base(_siteSettingAppService)
        {
        }

        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckToActivated();
            return await Task.FromResult<IActionResult>(Page());
        }
    }
}