using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODY.Cihazkapinda.ProductManagement;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.Web.Models.ProductManagementModals;

namespace ODY.Cihazkapinda.Web.Pages.Admin.ProductManagement.ProductPropertyTitles
{
    public class EditModalModel : CihazkapindaPageModel
    {
        #region PROPS
        [BindProperty]
        public ProductPropertyTitleEditModal productPropertyTitleEditModal { get; set; }

        #endregion PROPS

        #region SERVICES
        public IProductPropertyTitleAppService _productPropertyTitleAppService { get; set; }
        #endregion SERVICES
        public EditModalModel(ISiteSettingAppService siteSettingAppService,
            IProductPropertyTitleAppService productPropertyTitleAppService) : base(siteSettingAppService)
        {
            _productPropertyTitleAppService = productPropertyTitleAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync(Guid id)
        {
            await CheckAll();
            productPropertyTitleEditModal = ObjectMapper.Map<ProductPropertyTitleDto, ProductPropertyTitleEditModal>(
                    await _productPropertyTitleAppService.GetAsync(id)
                );
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            productPropertyTitleEditModal.TenantId = CurrentTenant.Id;
            var update = ObjectMapper.Map<ProductPropertyTitleEditModal, ProductPropertyTitleCreateUpdateDto>(productPropertyTitleEditModal);
            await _productPropertyTitleAppService.UpdateAsync(productPropertyTitleEditModal.Id, update);
            return NoContent();
        }
    }
}
