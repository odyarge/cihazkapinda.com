using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODY.Cihazkapinda.ProductManagement;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.Web.Models.ProductManagementModals;

namespace ODY.Cihazkapinda.Web.Pages.Admin.ProductManagement.ProductPropertyTemplate
{
    public class EditModalModel : CihazkapindaPageModel
    {
        #region PROPS
        [BindProperty]
        public ProductPropertyTemplateEditModal productPropertyTemplateEditModal { get; set; }

        #endregion PROPS

        #region SERVICES
        public IProductPropertyTemplateAppService _productPropertyTemplateAppService { get; set; }
        #endregion SERVICES
        public EditModalModel(ISiteSettingAppService siteSettingAppService,
            IProductPropertyTemplateAppService productPropertyTemplateAppService) : base(siteSettingAppService)
        {
            _productPropertyTemplateAppService = productPropertyTemplateAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync(Guid id)
        {
            await CheckAll();
            productPropertyTemplateEditModal = ObjectMapper.Map<ProductPropertyTemplateDto, ProductPropertyTemplateEditModal>(
                    await _productPropertyTemplateAppService.GetAsyncProductTemplateWithSub(id)
                );
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            productPropertyTemplateEditModal.TenantId = CurrentTenant.Id;
            var update = ObjectMapper.Map<ProductPropertyTemplateEditModal, ProductPropertyTemplateCreateUpdateDto>(productPropertyTemplateEditModal);
            await _productPropertyTemplateAppService.UpdateAsync(productPropertyTemplateEditModal.Id, update);
            return NoContent();
        }
    }
}