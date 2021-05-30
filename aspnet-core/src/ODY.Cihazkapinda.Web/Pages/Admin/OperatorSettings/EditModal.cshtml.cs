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
    public class EditModalModel : CihazkapindaPageModel
    {
        #region PROP
        [BindProperty]
        public OperatorSettingEditModal operatorSettingEditModal { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Image")]
        public IFormFile file { get; set; }
        #endregion PROP

        #region SERVICES
        public IOperatorSettingAppService _operatorSettingAppService { get; set; }
        #endregion SERVICES


        public EditModalModel(ISiteSettingAppService _siteSettingAppService,
            IOperatorSettingAppService operatorSettingAppService) : base(_siteSettingAppService)
        {
            _operatorSettingAppService = operatorSettingAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync(Guid id)
        {
            await CheckAll();
            operatorSettingEditModal = ObjectMapper.Map<OperatorSettingDto, OperatorSettingEditModal>(
                await _operatorSettingAppService.GetAsync(id)
                );
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            ValidateModel();

            //string tenantPath = CurrentTenant.Id == null ? "Host" : CurrentTenant.Id.ToString();
            string ignoreWWW = string.Empty;
            if (file.Length > 0)
            {
                string oldFilePath = "wwwroot\\" + operatorSettingEditModal.Image;
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                ignoreWWW = Path.Combine("host\\operators", file.FileName);
                string filePath = Path.Combine("wwwroot\\host\\operators", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            operatorSettingEditModal.TenantId = CurrentTenant.Id;
            operatorSettingEditModal.Image = ignoreWWW;
            var update = ObjectMapper.Map<OperatorSettingEditModal, OperatorSettingCreateUpdateDto>(operatorSettingEditModal);
            await _operatorSettingAppService.UpdateAsync(operatorSettingEditModal.Id, update);
            return await Task.FromResult<IActionResult>(Page());
        }
    }
}
