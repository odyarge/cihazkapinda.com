using Microsoft.AspNetCore.Mvc;
using ODY.Cihazkapinda.Categories;
using ODY.Cihazkapinda.GeneralSettings;
using ODY.Cihazkapinda.ProductManagement;
using ODY.Cihazkapinda.SiteSettings;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.Web.Pages
{
    public class IndexModel : CihazkapindaPageModel
    {
        #region PROPS
        public List<ProductDto> Products { get; set; }
        public string theme { get; set; }

        public GeneralSettingDto setting { get; set; }
        #endregion PROPS

        #region SERVICES
        private readonly IGeneralSettingAppService _generalSettingAppService;
        private readonly ISiteSettingAppService _siteSettingAppService;
        private readonly IProductAppService _productAppService;
        private readonly ICategoryAppService _categoryAppService;
        #endregion SERVICES
        public IndexModel(ISiteSettingAppService siteSettingAppService,
            IGeneralSettingAppService generalSettingAppService,
            IProductAppService productAppService,
            ICategoryAppService categoryAppService) : base(siteSettingAppService)
        {
            _generalSettingAppService = generalSettingAppService;
            _siteSettingAppService = siteSettingAppService;
            _productAppService = productAppService;
            _categoryAppService = categoryAppService;
        }

        public async virtual Task<IActionResult> OnGetAsync()
        {
            if (CurrentTenant.Id != null)
            {
                var checkActivated = await _siteSettingAppService.GetAsyncByTenantName(CurrentTenant.Name);
                if (checkActivated != null)
                {
                    bool activated = checkActivated.SITE_ACTIVATED;
                    bool installed = checkActivated.SITE_INSTALL;
                    if (installed == false)
                    {
                        return RedirectSafely("/Install/Index");
                    }
                    if (activated == false)
                    {
                        return RedirectSafely("/Error/Activate/");
                    }
                }
                else
                {
                    return RedirectSafely("/Error/404");
                }
            }

            theme = await _generalSettingAppService.GetAsyncTheme(CurrentTenant.Id);
            setting = await _generalSettingAppService.GetAsyncByTenant(CurrentTenant.Id);
            return await Task.FromResult<IActionResult>(Page());
        }
    }
}