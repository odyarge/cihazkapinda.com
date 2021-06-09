using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODY.Cihazkapinda.ProductManagement;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.Web.Models.ProductManagementModals;

namespace ODY.Cihazkapinda.Web.Pages.Admin.ProductManagement.ProductInfoTemplate
{
    public class CreateModalModel : CihazkapindaPageModel
    {
        [BindProperty]
        public ProductInfoTemplateCreateModal productInfoTemplateCreateModal { get; set; }

        [BindProperty]
        public IFormFile file { get; set; }

        public IProductInfoTemplateAppService _productInfoTemplateAppService { get; set; }
        public CreateModalModel(ISiteSettingAppService _siteSettingAppService,
            IProductInfoTemplateAppService productInfoTemplateAppService) : base(_siteSettingAppService)
        {
            _productInfoTemplateAppService = productInfoTemplateAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync(Guid id)
        {
            await CheckAll();
            productInfoTemplateCreateModal = new ProductInfoTemplateCreateModal();
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
                        tenantPath = "wwwroot\\host\\product-info";
                        ignoreWWW = Path.Combine("\\host\\product-info", file.FileName);
                        filePath = Path.Combine(tenantPath, file.FileName);
                    }
                    else
                    {
                        tenantPath = "wwwroot\\tenant\\" + CurrentTenant.Id.ToString() + "\\product-info";
                        ignoreWWW = Path.Combine("tenant\\" + CurrentTenant.Id.ToString() + "\\product-info", file.FileName);
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

            productInfoTemplateCreateModal.TenantId = CurrentTenant.Id;
            productInfoTemplateCreateModal.Image = ignoreWWW;
            var create = ObjectMapper.Map<ProductInfoTemplateCreateModal, ProductInfoTemplateCreateUpdateDto>(productInfoTemplateCreateModal);
            await _productInfoTemplateAppService.CreateAsync(create);

            return NoContent();
        }
    }
}
