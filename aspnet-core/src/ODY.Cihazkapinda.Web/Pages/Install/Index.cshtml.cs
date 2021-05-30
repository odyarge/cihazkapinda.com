using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ODY.Cihazkapinda.GeneralSettings;
using ODY.Cihazkapinda.Licenses;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.ThemeSettings;
using ODY.Cihazkapinda.Web.Models;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.Web.Pages.Install
{
    public class IndexModel : CihazkapindaPageModel
    {
        [BindProperty]
        public InstallModal installModal { get; set; }

        [BindProperty]
        public string currentTenant { get; set; }

        [BindProperty]
        public List<SelectListItem> themeList { get; set; }

        private readonly ISiteSettingAppService _siteSettingAppService;
        private readonly IGeneralSettingAppService _generalSettingAppService;
        private readonly ILicenseAppService _licenseAppService;
        private readonly IThemeSettingAppService _themeSettingAppService;
        public IndexModel(ISiteSettingAppService siteSettingAppService,
            IGeneralSettingAppService generalSettingAppService,
            ILicenseAppService licenseAppService,
            IThemeSettingAppService themeSettingAppService) : base(siteSettingAppService)
        {
            _siteSettingAppService = siteSettingAppService;
            _generalSettingAppService = generalSettingAppService;
            _licenseAppService = licenseAppService;
            _themeSettingAppService = themeSettingAppService;
        }

        public async virtual Task<IActionResult> OnGetAsync()
        {
            if(CurrentUser.IsAuthenticated == false)
                return RedirectToPage("/Account/Login");

            var checkActivated = await _siteSettingAppService.GetAsyncByTenantName(CurrentTenant.Name);
            if(checkActivated != null)
            {
                bool activated = checkActivated.SITE_INSTALL;
                if (activated == true)
                {
                    return RedirectToPage("/");
                }
            }
            else
            {
                return RedirectToPage("/Error/404");
            }
            
            installModal = new InstallModal();
            currentTenant = CurrentTenant.Name;
            await GetThemes();
            return await Task.FromResult<IActionResult>(Page());
        }

        public async virtual Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            return await Task.FromResult<IActionResult>(Page());
        }

        public async Task GetThemes()
        {
            PagedAndSortedResultRequestDto pagedAndSortedResultRequestDto = new PagedAndSortedResultRequestDto();
            var list = await _themeSettingAppService.GetListAsync(pagedAndSortedResultRequestDto);

            themeList = new List<SelectListItem>();
            foreach (var item in list.Items)
            {
                SelectListItem option = new SelectListItem
                {
                    Text = item.THEME_NAME,
                    Value = item.THEME_NAME
                };

                themeList.Add(option);
            }
        }
    }
}
