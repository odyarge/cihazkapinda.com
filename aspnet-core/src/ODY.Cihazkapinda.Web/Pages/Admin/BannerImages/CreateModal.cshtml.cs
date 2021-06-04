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
using ODY.Cihazkapinda.BannerImages;
using ODY.Cihazkapinda.BannerSettings;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.ThemeSettings;
using ODY.Cihazkapinda.Web.Models.BannerImageModals;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.Web.Pages.Admin.BannerImages
{
    public class CreateModalModel : CihazkapindaPageModel
    {
        #region PROP
        [BindProperty]
        public BannerImageCreateModal bannerImageCreateModal { get; set; }

        [BindProperty]
        public List<SelectListItem> bannerList { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Image")]
        public IFormFile file { get; set; }
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
        public async virtual Task<IActionResult> OnGetAsync(Guid id)
        {
            await CheckAll();
            proTheme = await _themeSettingAppService.GetAsyncProThemeControl(CurrentTenant.Id);
            bannerImageCreateModal = new BannerImageCreateModal();
            await GetBannerSettings();
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            ValidateModel();

            string ignoreWWW = string.Empty;
            string tenantPath = string.Empty;
            string oldFilePath = string.Empty;
            string filePath = string.Empty;

            if (file != null)
            {
                if (file.Length > 0)
                {
                    if (CurrentTenant.Id == null)
                    {
                        tenantPath = "wwwroot\\host\\banners";
                        ignoreWWW = Path.Combine("\\host\\banners", file.FileName);
                        filePath = Path.Combine(tenantPath, file.FileName);
                    }
                    else
                    {
                        tenantPath = "wwwroot\\tenant\\" + CurrentTenant.Id.ToString() + "\\banners";
                        ignoreWWW = Path.Combine("tenant\\" + CurrentTenant.Id.ToString() + "\\banners", file.FileName);
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

            bannerImageCreateModal.TenantId = CurrentTenant.Id;
            bannerImageCreateModal.Image = ignoreWWW;
            var create = ObjectMapper.Map<BannerImageCreateModal, BannerImageCreateUpdateDto>(bannerImageCreateModal);
            await _bannerImageAppService.CreateAsync(create);

            return NoContent();
        }


        public async Task GetBannerSettings()
        {
            var list = await _bannerSettingAppService.GetAllList();

            bannerList = new List<SelectListItem>();
            foreach (var item in list)
            {
                SelectListItem option = new SelectListItem
                {
                    Text = item.Title,
                    Value = item.Id.ToString()
                };

                bannerList.Add(option);
            }
        }
    }
}
