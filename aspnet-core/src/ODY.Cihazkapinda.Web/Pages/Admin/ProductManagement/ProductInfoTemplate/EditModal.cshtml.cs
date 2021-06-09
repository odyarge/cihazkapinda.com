using System;
using System.Collections.Generic;
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
    public class EditModalModel : CihazkapindaPageModel
    {
        [BindProperty]
        public ProductInfoTemplateEditModal productInfoTemplateEditModal { get; set; }

        [BindProperty]
        public IFormFile file { get; set; }

        public IProductInfoTemplateAppService _productInfoTemplateAppService { get; set; }
        public EditModalModel(ISiteSettingAppService _siteSettingAppService,
            IProductInfoTemplateAppService productInfoTemplateAppService) : base(_siteSettingAppService)
        {
            _productInfoTemplateAppService = productInfoTemplateAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync(Guid id)
        {
            await CheckAll();
            productInfoTemplateEditModal = ObjectMapper.Map<ProductInfoTemplateDto, ProductInfoTemplateEditModal>(
                    await _productInfoTemplateAppService.GetAsync(id)
                );
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
                        oldFilePath = tenantPath + "\\" + productInfoTemplateEditModal.Image;
                        filePath = Path.Combine(tenantPath, file.FileName);
                    }
                    else
                    {
                        tenantPath = "wwwroot\\tenant\\" + CurrentTenant.Id.ToString() + "\\product-info";
                        ignoreWWW = Path.Combine("tenant\\" + CurrentTenant.Id.ToString() + "\\product-info", file.FileName);
                        oldFilePath = tenantPath + "\\" + productInfoTemplateEditModal.Image;
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

            productInfoTemplateEditModal.TenantId = CurrentTenant.Id;
            productInfoTemplateEditModal.Image = ignoreWWW;
            var update = ObjectMapper.Map<ProductInfoTemplateEditModal, ProductInfoTemplateCreateUpdateDto>(productInfoTemplateEditModal);
            await _productInfoTemplateAppService.UpdateAsync(productInfoTemplateEditModal.Id, update);

            return NoContent();
        }
    }
}
