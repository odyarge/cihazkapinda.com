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
    public class EditModalModel : CihazkapindaPageModel
    {
        #region PROP
        [BindProperty]
        public ThemeSettingEditModal themeSettingEditModal { get; set; }

        [BindProperty]
        [Display(Name = "File")]
        public IFormFile file { get; set; }

        [BindProperty]
        [Display(Name = "NewTheme")]
        public bool newTheme { get; set; }
        #endregion PROP

        #region SERVICES
        public IThemeSettingAppService _themeSettingAppService { get; set; }
        #endregion SERVICES


        public EditModalModel(ISiteSettingAppService _siteSettingAppService,
            IThemeSettingAppService themeSettingAppService) : base(_siteSettingAppService)
        {
            _themeSettingAppService = themeSettingAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync(Guid id)
        {
            await CheckAll();
            themeSettingEditModal = ObjectMapper.Map<ThemeSettingDto, ThemeSettingEditModal>(
                    await _themeSettingAppService.GetAsync(id)
                );
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            ValidateModel();

            //string tenantPath = CurrentTenant.Id == null ? "Host" : CurrentTenant.Id.ToString();
            string ignoreWWW = themeSettingEditModal.THEME_PATH;
            if(file != null)
            {
                if (file.Length > 0)
                {
                    ignoreWWW = Path.Combine("Themes\\Stisla\\Layouts", file.FileName);
                    if (newTheme == true)
                    {
                        string oldFilePath = themeSettingEditModal.THEME_PATH;
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }

                        string filePath = Path.Combine("Themes\\Stisla\\Layouts", file.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }
            }
            
            themeSettingEditModal.TenantId = CurrentTenant.Id;
            themeSettingEditModal.THEME_PATH = ignoreWWW;
            var update = ObjectMapper.Map<ThemeSettingEditModal, ThemeSettingCreateUpdateDto>(themeSettingEditModal);
            await _themeSettingAppService.UpdateAsync(themeSettingEditModal.Id, update);
            return await Task.FromResult<IActionResult>(Page());
        }
    }
}
