using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODY.Cihazkapinda.BannerImages;
using ODY.Cihazkapinda.BannerSettings;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.ThemeSettings;
using ODY.Cihazkapinda.Web.Models.BannerImageModals;
using ODY.Cihazkapinda.Web.Models.BannerSettingModals;

namespace ODY.Cihazkapinda.Web.Pages.Admin.BannerSettings
{
    public class CreateModalModel : CihazkapindaPageModel
    {
        #region PROP
        [BindProperty]
        public BannerSettingCreateModal bannerSettingCreateModal { get; set; }
        
        [BindProperty]
        [Required]
        public List<string> titles { get; set; }
        
        [BindProperty]
        [Required]
        public List<string> contents { get; set; }
        
        [BindProperty]
        [Required]
        public List<IFormFile> files { get; set; }
        #endregion PROP

        #region SERVICES
        public IBannerSettingAppService _bannerSettingAppService { get; set; }
        public IBannerImageAppService _bannerImageAppService { get; set; }
        public IThemeSettingAppService _themeSettingAppService { get; set; }
        #endregion SERVICES

        public bool proTheme { get; set; }

        public CreateModalModel(ISiteSettingAppService _siteSettingAppService,
            IBannerSettingAppService bannerSettingAppService,
            IBannerImageAppService bannerImageAppService,
            IThemeSettingAppService themeSettingAppService) : base(_siteSettingAppService)
        {
            _bannerSettingAppService = bannerSettingAppService;
            _themeSettingAppService = themeSettingAppService;
            _bannerImageAppService = bannerImageAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckAll();
            proTheme = await _themeSettingAppService.GetAsyncProThemeControl(CurrentTenant.Id);
            bannerSettingCreateModal = new BannerSettingCreateModal();
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync(List<IFormFile> files)
        {
            ValidateModel();

            List<string> ignoreWWW = new List<string>();
            string tenantPath = string.Empty;
            string oldFilePath = string.Empty;
            string filePath = string.Empty;

            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        if (CurrentTenant.Id == null)
                        {
                            tenantPath = "wwwroot\\host\\banners";
                            ignoreWWW.Add(Path.Combine("\\host\\banners", file.FileName));
                            filePath = Path.Combine(tenantPath, file.FileName);
                        }
                        else
                        {
                            tenantPath = "wwwroot\\tenant\\" + CurrentTenant.Id.ToString() + "\\banners";
                            ignoreWWW.Add(Path.Combine("tenant\\" + CurrentTenant.Id.ToString() + "\\banners", file.FileName));
                            filePath = Path.Combine(tenantPath, file.FileName);
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
                }
            }

            bannerSettingCreateModal.TenantId = CurrentTenant.Id;
            var create = ObjectMapper.Map<BannerSettingCreateModal, BannerSettingCreateUpdateDto>(bannerSettingCreateModal);
            var result = await _bannerSettingAppService.CreateAsync(create);

            for (int i = 0; i < ignoreWWW.Count; i++)
            {
                BannerImageCreateUpdateDto createImage = new BannerImageCreateUpdateDto()
                {
                    TenantId = CurrentTenant.Id,
                    BannerSettingId = result.Id,
                    Image = ignoreWWW[i],
                    Title = titles[i],
                    Content = contents[i]
                };
                await _bannerImageAppService.CreateAsync(createImage);
            }
            return NoContent();
        }
    }
}
