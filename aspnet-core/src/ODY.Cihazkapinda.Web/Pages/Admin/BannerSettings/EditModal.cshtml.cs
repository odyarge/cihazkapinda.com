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
using ODY.Cihazkapinda.Web.Models.BannerSettingModals;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.Web.Pages.Admin.BannerSettings
{
    public class EditModalModel : CihazkapindaPageModel
    {
        #region PROP
        [BindProperty]
        public BannerSettingEditModal bannerSettingEditModal { get; set; }
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
            bannerSettingEditModal = ObjectMapper.Map<BannerSettingDto, BannerSettingEditModal>(
                    await _bannerSettingAppService.GetAsyncBannerWithImage(id)
                );
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            PagedAndSortedResultRequestDto dto = new PagedAndSortedResultRequestDto();
            var controlList = await _bannerSettingAppService.GetListAsync(dto);
            foreach (var item in controlList.Items)
            {
                item.Active = false;
                var updateActive = ObjectMapper.Map<BannerSettingDto, BannerSettingCreateUpdateDto>(item);
                await _bannerSettingAppService.UpdateAsync(item.Id, updateActive);
            }

            bannerSettingEditModal.TenantId = CurrentTenant.Id;
            var update = ObjectMapper.Map<BannerSettingEditModal, BannerSettingCreateUpdateDto>(bannerSettingEditModal);
            await _bannerSettingAppService.UpdateAsync(bannerSettingEditModal.Id, update);

            return NoContent();
        }
    }
}
