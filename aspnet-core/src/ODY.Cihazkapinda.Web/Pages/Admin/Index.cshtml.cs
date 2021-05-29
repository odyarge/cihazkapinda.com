using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODY.Cihazkapinda.SiteSettings;

namespace ODY.Cihazkapinda.Web.Pages.Admin
{
    public class IndexModel : CihazkapindaPageModel
    {
        public IndexModel(ISiteSettingAppService _siteSettingAppService) : base(_siteSettingAppService)
        {
        }
        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckAll();
            return await Task.FromResult<IActionResult>(Page());
        }
    }
}
