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
    public class EditModalModel : CihazkapindaPageModel
    {
        #region PROPS
        [BindProperty]
        public SiteSettingEditModal siteSettingEditModal { get; set; }

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
        public EditModalModel(ISiteSettingAppService siteSettingAppService,
            IOperatorSettingAppService operatorSettingAppService,
            ITenantAppService tenantAppService) : base(siteSettingAppService)
        {
            _siteSettingAppService = siteSettingAppService;
            _operatorSettingAppService = operatorSettingAppService;
            _tenantAppService = tenantAppService;
        }

        public async virtual Task<IActionResult> OnGetAsync(Guid id)
        {
            await CheckAll();
            siteSettingEditModal = ObjectMapper.Map<SiteSettingDto, SiteSettingEditModal>(
                await _siteSettingAppService.GetAsync(id)
                );
            await GetOperators(siteSettingEditModal.SITE_OPERATOR);
            await GetTenants(siteSettingEditModal.SITE_OWNER);
            return await Task.FromResult<IActionResult>(Page());
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            siteSettingEditModal.TenantId = CurrentTenant.Id;
            var update = ObjectMapper.Map<SiteSettingEditModal, SiteSettingCreateUpdateDto>(siteSettingEditModal);
            await _siteSettingAppService.UpdateAsync(siteSettingEditModal.Id, update);
            return await Task.FromResult<IActionResult>(Page());
        }

        public async Task GetOperators(string selected)
        {
            PagedAndSortedResultRequestDto pagedAndSortedResultRequestDto = new PagedAndSortedResultRequestDto();
            var list = await _operatorSettingAppService.GetListAsync(pagedAndSortedResultRequestDto);

            operatorList = new List<SelectListItem>();
            foreach (var item in list.Items)
            {
                SelectListItem option;
                if (item.OperatorName == selected)
                {
                    option = new SelectListItem
                    {
                        Text = item.OperatorName,
                        Value = item.OperatorName,
                        Selected = true
                    };
                }
                else
                {
                    option = new SelectListItem
                    {
                        Text = item.OperatorName,
                        Value = item.OperatorName
                    };
                }

                operatorList.Add(option);
            }
        }

        public async Task GetTenants(string selected)
        {
            GetTenantsInput input = new GetTenantsInput();
            var list = await _tenantAppService.GetListAsync(input);

            tenantList = new List<SelectListItem>();
            foreach (var item in list.Items)
            {
                SelectListItem option;
                if (item.Name == selected)
                {
                    option = new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Name,
                        Selected = true
                    };
                }
                else
                {
                    option = new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Name
                    };
                }

                tenantList.Add(option);
            }
        }
    }
}
