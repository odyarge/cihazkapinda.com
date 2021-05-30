using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ODY.Cihazkapinda.OperatorSettings;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.Web.Models.SiteSettingModals;
using Volo.Abp.Application.Dtos;
using Volo.Abp.TenantManagement;

namespace ODY.Cihazkapinda.Web.Pages.Admin.SiteSettings
{
    public class CreateModalModel : CihazkapindaPageModel
    {
        #region PROPS
        [BindProperty]
        public SiteSettingCreateModal siteSettingCreateModal { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Operator")]
        public List<SelectListItem> operatorList { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Tenant")]
        public List<SelectListItem> tenantList { get; set; }
        #endregion PROPS

        #region SERVICES
        public ISiteSettingAppService _siteSettingAppService { get; set; }
        public IOperatorSettingAppService _operatorSettingAppService { get; set; }
        protected ITenantAppService _tenantAppService { get; }
        #endregion SERVICES
        public CreateModalModel(ISiteSettingAppService siteSettingAppService,
            IOperatorSettingAppService operatorSettingAppService,
            ITenantAppService tenantAppService) : base(siteSettingAppService)
        {
            _siteSettingAppService = siteSettingAppService;
            _operatorSettingAppService = operatorSettingAppService;
            _tenantAppService = tenantAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync()
        {
            await CheckAll();
            siteSettingCreateModal = new SiteSettingCreateModal();
            siteSettingCreateModal.SITE_LICENSE_FINISH_TIME = DateTime.Now;
            siteSettingCreateModal.SITE_LICENSE = GuidGenerator.Create().ToString();
            await GetOperators();
            await GetTenants();
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            siteSettingCreateModal.TenantId = CurrentTenant.Id;
            var create = ObjectMapper.Map<SiteSettingCreateModal, SiteSettingCreateUpdateDto>(siteSettingCreateModal);
            await _siteSettingAppService.CreateAsync(create);
            return await Task.FromResult<IActionResult>(Page());
        }

        public async Task GetOperators()
        {
            PagedAndSortedResultRequestDto pagedAndSortedResultRequestDto = new PagedAndSortedResultRequestDto();
            var list = await _operatorSettingAppService.GetListAsync(pagedAndSortedResultRequestDto);

            operatorList = new List<SelectListItem>();
            foreach (var item in list.Items)
            {
                SelectListItem option = new SelectListItem
                {
                    Text = item.OperatorName,
                    Value = item.OperatorName
                };

                operatorList.Add(option);
            }
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
