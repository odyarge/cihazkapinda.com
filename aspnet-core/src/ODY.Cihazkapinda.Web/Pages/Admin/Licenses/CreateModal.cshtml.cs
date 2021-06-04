using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ODY.Cihazkapinda.Licenses;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.Web.Models.LicenseModals;
using Volo.Abp.TenantManagement;

namespace ODY.Cihazkapinda.Web.Pages.Admin.Licenses
{
    public class CreateModalModel : CihazkapindaPageModel
    {
        #region PROPS
        [BindProperty]
        public LicenseCreateModal licenseCreateModal { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Tenant")]
        public List<SelectListItem> tenantList { get; set; }
        #endregion PROPS

        #region SERVICES
        public ISiteSettingAppService _siteSettingAppService { get; set; }
        public ILicenseAppService _licenseAppService { get; set; }
        protected ITenantAppService _tenantAppService { get; }
        #endregion SERVICES
        public CreateModalModel(ISiteSettingAppService siteSettingAppService,
            ILicenseAppService licenseAppService,
            ITenantAppService tenantAppService) : base(siteSettingAppService)
        {
            _siteSettingAppService = siteSettingAppService;
            _licenseAppService = licenseAppService;
            _tenantAppService = tenantAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckAll();
            licenseCreateModal = new LicenseCreateModal();
            await GetTenants();
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            licenseCreateModal.TenantId = CurrentTenant.Id;
            var create = ObjectMapper.Map<LicenseCreateModal, LicenseCreateUpdateDto>(licenseCreateModal);
            await _licenseAppService.CreateAsync(create);
            return NoContent();
        }

        public async Task GetTenants()
        {
            GetTenantsInput input = new GetTenantsInput();
            var list = await _tenantAppService.GetListAsync(input);

            tenantList = new List<SelectListItem>();
            foreach (var item in list.Items)
            {
                SelectListItem option = new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Name
                };

                tenantList.Add(option);
            }
        }
    }
}
