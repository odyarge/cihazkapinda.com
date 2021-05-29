using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ODY.Cihazkapinda.SiteSettings;
using Volo.Abp.Identity.Web.Pages.Identity;

namespace ODY.Cihazkapinda.Web.Pages.Identity.Users
{
    public class IndexModel : IdentityPageModel
    {
        private readonly ISiteSettingAppService _siteSettingAppService;
        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckToActivated();
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual Task<IActionResult> OnPostAsync()
        {
            return Task.FromResult<IActionResult>(Page());
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
    }
}
