using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODY.Cihazkapinda.OperatorSettings;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.Web.Models.OperatorSettingModals;

namespace ODY.Cihazkapinda.Web.Pages.Admin.OperatorSettings
{
    public class CreateModalModel : CihazkapindaPageModel
    {
        #region PROP
        [BindProperty]
        public OperatorSettingCreateModal operatorSettingCreateModal { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Image")]
        public IFormFile file { get; set; }
        #endregion PROP

        #region SERVICES
        public IOperatorSettingAppService _operatorSettingAppService { get; set; }
        #endregion SERVICES


        public CreateModalModel(ISiteSettingAppService _siteSettingAppService,
            IOperatorSettingAppService operatorSettingAppService) : base(_siteSettingAppService)
        {
            _operatorSettingAppService = operatorSettingAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckAll();
            operatorSettingCreateModal = new OperatorSettingCreateModal();
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            ValidateModel();

            //string tenantPath = CurrentTenant.Id == null ? "Host" : CurrentTenant.Id.ToString();
            string ignoreWWW = string.Empty;
            if (file.Length > 0)
            {
                ignoreWWW = Path.Combine("host\\operators", file.FileName);
                string filePath = Path.Combine("wwwroot\\host\\operators", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            operatorSettingCreateModal.TenantId = CurrentTenant.Id;
            operatorSettingCreateModal.Image = ignoreWWW;
            var create = ObjectMapper.Map<OperatorSettingCreateModal, OperatorSettingCreateUpdateDto>(operatorSettingCreateModal);
            await _operatorSettingAppService.CreateAsync(create);
            return NoContent();
        }
    }
}
