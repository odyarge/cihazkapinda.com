using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.ThemeSettings;
using ODY.Cihazkapinda.Web.Models.ThemeSettingModals;

namespace ODY.Cihazkapinda.Web.Pages.Admin.ThemeSettings
{
    public class CreateModalModel : CihazkapindaPageModel
    {
        #region PROP
        [BindProperty]
        public ThemeSettingCreateModal themeSettingCreateModal { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "File")]
        public IFormFile file { get; set; }

        [BindProperty]
        [Display(Name = "NewTheme")]
        public bool newTheme { get; set; }
        #endregion PROP

        #region SERVICES
        public IThemeSettingAppService _themeSettingAppService { get; set; }
        #endregion SERVICES


        public CreateModalModel(ISiteSettingAppService _siteSettingAppService,
            IThemeSettingAppService themeSettingAppService) : base(_siteSettingAppService)
        {
            _themeSettingAppService = themeSettingAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckAll();
            themeSettingCreateModal = new ThemeSettingCreateModal();
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            ValidateModel();

            //string tenantPath = CurrentTenant.Id == null ? "Host" : CurrentTenant.Id.ToString();
            string ignoreWWW = string.Empty;
            if (file.Length > 0)
            {
                ignoreWWW = Path.Combine("Themes\\Stisla\\Layouts", file.FileName);
                if (newTheme == true)
                {
                    string filePath = Path.Combine("Themes\\Stisla\\Layouts", file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            themeSettingCreateModal.TenantId = CurrentTenant.Id;
            themeSettingCreateModal.THEME_PATH = ignoreWWW;
            var create = ObjectMapper.Map<ThemeSettingCreateModal, ThemeSettingCreateUpdateDto>(themeSettingCreateModal);
            await _themeSettingAppService.CreateAsync(create);
            return await Task.FromResult<IActionResult>(Page());
        }
    }
}
