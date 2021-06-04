using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODY.Cihazkapinda.ProductManagement;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.Web.Models.ProductManagementModals;

namespace ODY.Cihazkapinda.Web.Pages.Admin.ProductManagement.ProductPropertyTitles
{
    public class CreateModalModel : CihazkapindaPageModel
    {
        #region PROPS
        [BindProperty]
        public ProductPropertyTitleCreateModal productPropertyTitleCreateModal { get; set; }

        #endregion PROPS

        #region SERVICES
        public IProductPropertyTitleAppService _productPropertyTitleAppService { get; set; }
        #endregion SERVICES
        public CreateModalModel(ISiteSettingAppService siteSettingAppService,
            IProductPropertyTitleAppService productPropertyTitleAppService) : base(siteSettingAppService)
        {
            _productPropertyTitleAppService = productPropertyTitleAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckAll();
            productPropertyTitleCreateModal = new ProductPropertyTitleCreateModal();
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            productPropertyTitleCreateModal.TenantId = CurrentTenant.Id;
            var create = ObjectMapper.Map<ProductPropertyTitleCreateModal, ProductPropertyTitleCreateUpdateDto>(productPropertyTitleCreateModal);
            await _productPropertyTitleAppService.CreateAsync(create);
            return NoContent();
        }
    }
}
