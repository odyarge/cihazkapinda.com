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
    public class EditModalModel : CihazkapindaPageModel
    {
        #region PROP
        [BindProperty]
        public BannerImageEditModal bannerImageEditModal { get; set; }

        [BindProperty]
        public List<SelectListItem> bannerList { get; set; }

        [BindProperty]
        [Display(Name = "Image")]
        public IFormFile file { get; set; }
        #endregion PROP

        #region SERVICES
        public IBannerSettingAppService _bannerSettingAppService { get; set; }
        public IBannerImageAppService _bannerImageAppService { get; set; }
        public IThemeSettingAppService _themeSettingAppService { get; set; }
        #endregion SERVICES

        public bool proTheme { get; set; }

        public EditModalModel(ISiteSettingAppService _siteSettingAppService,
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
            bannerImageEditModal = ObjectMapper.Map<BannerImageDto, BannerImageEditModal>(
                    await _bannerImageAppService.GetAsync(id)
                );
            await GetBannerSettings(bannerImageEditModal.BannerSettingId);
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            ValidateModel();

            string ignoreWWW = bannerImageEditModal.Image;
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
                        oldFilePath = tenantPath + "\\" + bannerImageEditModal.Image;
                        filePath = Path.Combine(tenantPath, file.FileName);
                    }
                    else
                    {
                        tenantPath = "wwwroot\\tenant\\" + CurrentTenant.Id.ToString() + "\\banners";
                        ignoreWWW = Path.Combine("tenant\\" + CurrentTenant.Id.ToString() + "\\banners", file.FileName);
                        oldFilePath = tenantPath + "\\" + bannerImageEditModal.Image;
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
            }

            bannerImageEditModal.TenantId = CurrentTenant.Id;
            bannerImageEditModal.Image = ignoreWWW;
            var update = ObjectMapper.Map<BannerImageEditModal, BannerImageCreateUpdateDto>(bannerImageEditModal);
            await _bannerImageAppService.UpdateAsync(bannerImageEditModal.Id, update);

            return NoContent();
        }


        public async Task GetBannerSettings(Guid selected)
        {
            PagedAndSortedResultRequestDto dto = new PagedAndSortedResultRequestDto();
            var list = await _bannerSettingAppService.GetListAsync(dto);

            bannerList = new List<SelectListItem>();
            foreach (var item in list.Items)
            {
                SelectListItem option;
                if (item.Id == selected)
                {
                    option = new SelectListItem
                    {
                        Text = item.Title,
                        Value = item.Id.ToString(),
                        Selected = true
                    };
                }
                else
                {
                    option = new SelectListItem
                    {
                        Text = item.Title,
                        Value = item.Id.ToString()
                    };
                }

                bannerList.Add(option);
            }
        }
    }
}
