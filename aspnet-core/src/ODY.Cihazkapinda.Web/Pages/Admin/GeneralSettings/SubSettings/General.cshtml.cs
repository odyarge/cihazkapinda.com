using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ODY.Cihazkapinda.GeneralSettings;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.ThemeSettings;
using ODY.Cihazkapinda.Web.Models.GeneralSettingModals;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.Web.Pages.Admin.GeneralSettings.SubSettings
{
    public class GeneralModel : CihazkapindaPageModel
    {
        #region PROPS
        [BindProperty]
        public GeneralSettingEditModal generalSettingEditModal { get; set; }

        [BindProperty]
        [Display(Name = "Logo")]
        public IFormFile file { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Themes")]
        public List<SelectListItem> themeList { get; set; }
        #endregion PROPS

        #region SERVICES
        public IGeneralSettingAppService _generalSettingAppService { get; set; }
        public IThemeSettingAppService _themeSettingAppService { get; set; }
        #endregion SERVICES
        public GeneralModel(ISiteSettingAppService _siteSettingAppService,
            IGeneralSettingAppService generalSettingAppService,
            IThemeSettingAppService themeSettingAppService) : base(_siteSettingAppService)
        {
            _generalSettingAppService = generalSettingAppService;
            _themeSettingAppService = themeSettingAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckAll();

            generalSettingEditModal = ObjectMapper.Map<GeneralSettingDto, GeneralSettingEditModal>(
                await _generalSettingAppService.GetAsyncByTenant(CurrentTenant.Id)
                );
            await GetThemes(generalSettingEditModal.SiteTheme);
            return await Task.FromResult<IActionResult>(Page());
        }


        public async virtual Task<IActionResult> OnPostAsync(IFormFile file)
        {
            var test = ModelState.IsValid;
            ValidateModel();
            string ignoreWWW = string.Empty;
            string tenantPath = string.Empty;
            string oldFilePath = string.Empty;
            string filePath = string.Empty;
            if (file.Length > 0)
            {
                if (CurrentTenant.Id == null)
                {
                    tenantPath = "wwwroot\\host\\logo";
                    ignoreWWW = Path.Combine("\\host\\logo", file.FileName);
                    oldFilePath = tenantPath + "\\" + generalSettingEditModal.Logo;
                    filePath = Path.Combine(tenantPath, file.FileName);
                }
                else
                {
                    tenantPath = "wwwroot\\tenant\\" + CurrentTenant.Id.ToString() + "\\logo";
                    ignoreWWW = Path.Combine("tenant\\" + CurrentTenant.Id.ToString() + "\\logo", file.FileName);
                    oldFilePath = tenantPath + "\\" + generalSettingEditModal.Logo;
                    filePath = Path.Combine(tenantPath, file.FileName);
                }

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
                if (!Directory.Exists(tenantPath))
                {
                    Directory.CreateDirectory(tenantPath);
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            generalSettingEditModal.TenantId = CurrentTenant.Id;
            generalSettingEditModal.Logo = ignoreWWW;
            var update = ObjectMapper.Map<GeneralSettingEditModal, GeneralSettingCreateUpdateDto>(generalSettingEditModal);
            await _generalSettingAppService.UpdateAsync(generalSettingEditModal.Id, update);

            //D�zenlenecek
            return RedirectToPage("../Index");
        }


        public async Task GetThemes(string selected)
        {
            PagedAndSortedResultRequestDto pagedAndSortedResultRequestDto = new PagedAndSortedResultRequestDto();
            var list = await _themeSettingAppService.GetListAsync(pagedAndSortedResultRequestDto);

            themeList = new List<SelectListItem>();
            foreach (var item in list.Items)
            {
                SelectListItem option;
                if (item.THEME_NAME == selected)
                {
                    option = new SelectListItem
                    {
                        Text = item.THEME_NAME,
                        Value = item.THEME_NAME,
                        Selected = true
                    };
                }
                else
                {
                    option = new SelectListItem
                    {
                        Text = item.THEME_NAME,
                        Value = item.THEME_NAME
                    };
                }

                themeList.Add(option);
            }
        }
    }
}
