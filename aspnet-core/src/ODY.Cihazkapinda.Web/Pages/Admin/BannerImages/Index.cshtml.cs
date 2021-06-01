using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.ThemeSettings;

namespace ODY.Cihazkapinda.Web.Pages.Admin.BannerImages
{
    public class IndexModel : CihazkapindaPageModel
    {
        public IThemeSettingAppService _themeSettingAppService { get; set; }

        public bool proTheme { get; set; }
        public long bannerCount { get; set; }
        public IndexModel(ISiteSettingAppService _siteSettingAppService,
            IThemeSettingAppService themeSettingAppService) : base(_siteSettingAppService)
        {
            _themeSettingAppService = themeSettingAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckAll();
            proTheme = await _themeSettingAppService.GetAsyncProThemeControl(CurrentTenant.Id);
            return await Task.FromResult<IActionResult>(Page());
        }
    }
}
