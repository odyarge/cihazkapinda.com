using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODY.Cihazkapinda.SiteSettings;

namespace ODY.Cihazkapinda.Web.Pages.Admin.Chat
{
    public class IndexModel : CihazkapindaPageModel
    {
        public string TenantName { get; set; }
        public IndexModel(ISiteSettingAppService _siteSettingAppService) : base(_siteSettingAppService)
        {
        }
        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckAll();
            TenantName = CurrentTenant.Name;
            return await Task.FromResult<IActionResult>(Page());
        }
    }
}