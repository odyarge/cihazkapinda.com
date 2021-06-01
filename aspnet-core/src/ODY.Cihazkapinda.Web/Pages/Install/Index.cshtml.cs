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
using ODY.Cihazkapinda.Web.Models.GeneralSettingModals;
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
        public string license { get; set; }

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
            if (CurrentUser.IsAuthenticated == false)
                return RedirectSafely("/Account/Login");

            var checkActivated = await _siteSettingAppService.GetAsyncByTenantName(CurrentTenant.Name);
            if (checkActivated != null)
            {
                bool activated = checkActivated.SITE_INSTALL;
                if (activated == true)
                {
                    return RedirectSafely("/");
                }
            }
            else
            {
                return RedirectSafely("/Error/404");
            }

            installModal = new InstallModal();
            currentTenant = CurrentTenant.Name;
            await GetThemes();
            return await Task.FromResult<IActionResult>(Page());
        }

        public async virtual Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            installModal.generalSettingCreateModal.Logo = "Default";
            installModal.generalSettingCreateModal.TenantId = CurrentTenant.Id;
            var generalSettingCreate = ObjectMapper.Map<GeneralSettingCreateModal, GeneralSettingCreateUpdateDto>(installModal.generalSettingCreateModal);
            await _generalSettingAppService.CreateAsync(generalSettingCreate);

            var siteSetting = await _siteSettingAppService.UpdateInstall(license, CurrentTenant.Name);
            if(siteSetting == true)
            {
                var old_license = await _licenseAppService.GetCheckLicense(license, CurrentTenant.Name);
                await _licenseAppService.DeleteLicense(old_license.Id);
            }

            return RedirectSafely("/");
        }

        public async Task GetThemes()
        {
            var list = await _themeSettingAppService.GetListAsyncAllThemes();

            themeList = new List<SelectListItem>();
            foreach (var item in list)
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
