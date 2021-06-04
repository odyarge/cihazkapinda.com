using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ODY.Cihazkapinda.ProductManagement;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.Web.Models.ProductManagementModals;

namespace ODY.Cihazkapinda.Web.Pages.Admin.ProductManagement.ProductPropertyTemplate
{
    public class CreateModalModel : CihazkapindaPageModel
    {
        #region PROPS
        [BindProperty]
        public ProductPropertyTemplateCreateModal productPropertyTemplateCreateModal { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Key")]
        public List<string> keys { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Value")]
        public List<string> values { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Title")]
        public List<string> titles { get; set; }


        [BindProperty]
        public List<SelectListItem> SelectPropTitle { get; set; }
        #endregion PROPS

        #region SERVICES
        public IProductPropertyTitleAppService _productPropertyTitleAppService { get; set; }
        public IProductPropertyTemplateAppService _productPropertyTemplateAppService { get; set; }
        public IProductPropertySubTemplateAppService _productPropertySubTemplateAppService { get; set; }
        #endregion SERVICES
        public CreateModalModel(ISiteSettingAppService siteSettingAppService,
            IProductPropertyTitleAppService productPropertyTitleAppService,
            IProductPropertyTemplateAppService productPropertyTemplateAppService,
            IProductPropertySubTemplateAppService productPropertySubTemplateAppService) : base(siteSettingAppService)
        {
            _productPropertyTitleAppService = productPropertyTitleAppService;
            _productPropertyTemplateAppService = productPropertyTemplateAppService;
            _productPropertySubTemplateAppService = productPropertySubTemplateAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckAll();
            productPropertyTemplateCreateModal = new ProductPropertyTemplateCreateModal();
            await GetPropTitles();
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            productPropertyTemplateCreateModal.TenantId = CurrentTenant.Id;
            var create = ObjectMapper.Map<ProductPropertyTemplateCreateModal, ProductPropertyTemplateCreateUpdateDto>(productPropertyTemplateCreateModal);
            var result = await _productPropertyTemplateAppService.CreateAsync(create);

            for (int i = 0; i < keys.Count; i++)
            {
                ProductPropertySubTemplateCreateUpdateDto createSubTemplate = new ProductPropertySubTemplateCreateUpdateDto()
                {
                    TenantId = CurrentTenant.Id,
                    ProductPropertyTemplateId = result.Id,
                    KEY = keys[i],
                    VALUE = values[i],
                    TITLE = titles[i]
                };
                await _productPropertySubTemplateAppService.CreateAsync(createSubTemplate);
            }
            return NoContent();
        }

        public async Task GetPropTitles()
        {
            var list = await _productPropertyTitleAppService.GetAllList();
            SelectPropTitle = new List<SelectListItem>();
            SelectListItem empty = new SelectListItem
            {
                Text = "Seçiniz.",
                Value = "choise"
            };
            SelectPropTitle.Add(empty);

            foreach (var item in list)
            {
                SelectListItem newItem = new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Name
                    };

                SelectPropTitle.Add(newItem);
            }
        }
    }
}