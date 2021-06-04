using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ODY.Cihazkapinda.ProductManagement;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.Web.Models.ProductManagementModals;

namespace ODY.Cihazkapinda.Web.Pages.Admin.ProductManagement.ProductPropertySubTemplate
{
    public class EditModalModel : CihazkapindaPageModel
    {
        #region PROPS
        [BindProperty]
        public ProductPropertySubTemplateEditModal productPropertySubTemplateEditModal { get; set; }

        [BindProperty]
        public List<SelectListItem> productPropertyList { get; set; }
        #endregion PROPS

        #region SERVICES
        public IProductPropertyTemplateAppService _productPropertyTemplateAppService { get; set; }
        public IProductPropertySubTemplateAppService _productPropertySubTemplateAppService { get; set; }
        #endregion SERVICES
        public EditModalModel(ISiteSettingAppService siteSettingAppService,
            IProductPropertyTemplateAppService productPropertyTemplateAppService,
            IProductPropertySubTemplateAppService productPropertySubTemplateAppService) : base(siteSettingAppService)
        {
            _productPropertyTemplateAppService = productPropertyTemplateAppService;
            _productPropertySubTemplateAppService = productPropertySubTemplateAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync(Guid id)
        {
            await CheckAll();
            productPropertySubTemplateEditModal = ObjectMapper.Map<ProductPropertySubTemplateDto, ProductPropertySubTemplateEditModal>(
                    await _productPropertySubTemplateAppService.GetAsync(id)
                );
            await GetProductPropertyTemplate(productPropertySubTemplateEditModal.Id);
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            productPropertySubTemplateEditModal.TenantId = CurrentTenant.Id;
            var update = ObjectMapper.Map<ProductPropertySubTemplateEditModal, ProductPropertySubTemplateCreateUpdateDto>(productPropertySubTemplateEditModal);
            var result = await _productPropertySubTemplateAppService.UpdateAsync(productPropertySubTemplateEditModal.Id, update);

            return NoContent();
        }


        public async Task GetProductPropertyTemplate(Guid selected)
        {
            var list = await _productPropertyTemplateAppService.GetAllList();

            productPropertyList = new List<SelectListItem>();
            foreach (var item in list)
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

                productPropertyList.Add(option);
            }
        }
    }
}