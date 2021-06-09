using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODY.Cihazkapinda.GeneralSettings;
using ODY.Cihazkapinda.ProductManagement;
using ODY.Cihazkapinda.SiteSettings;

namespace ODY.Cihazkapinda.Web.Pages.Product.ProductDetail
{
    public class IndexModel : CihazkapindaPageModel
    {
        public ProductDto Product { get; set; }
        public string theme { get; set; }
        public GeneralSettingDto setting { get; set; }

        private readonly IProductAppService _productAppService;
        private readonly IGeneralSettingAppService _generalSettingAppService;
        private readonly ISiteSettingAppService _siteSettingAppService;
        public IndexModel(ISiteSettingAppService siteSettingAppService,
            IGeneralSettingAppService generalSettingAppService,
            IProductAppService productAppService) : base(siteSettingAppService)
        {
            _generalSettingAppService = generalSettingAppService;
            _siteSettingAppService = siteSettingAppService;
            _productAppService = productAppService;
        }

        public async virtual Task<IActionResult> OnGetAsync(Guid? id)
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
            if (id == null)
                return RedirectSafely("/");

            Product = await _productAppService.GetAsyncProductWithDetail((Guid)id);
            theme = await _generalSettingAppService.GetAsyncTheme(CurrentTenant.Id);
            setting = await _generalSettingAppService.GetAsyncByTenant(CurrentTenant.Id);
            return await Task.FromResult<IActionResult>(Page());
        }
    }
}
