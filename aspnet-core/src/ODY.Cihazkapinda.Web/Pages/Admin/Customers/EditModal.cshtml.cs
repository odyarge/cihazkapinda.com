using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ODY.Cihazkapinda.Customers;
using ODY.Cihazkapinda.SiteSettings;
using ODY.Cihazkapinda.Web.Models.CustomerModals;

namespace ODY.Cihazkapinda.Web.Pages.Admin.Customers
{
    public class EditModalModel : CihazkapindaPageModel
    {
        [BindProperty]
        public CustomerEditModal customerEditModal { get; set; }

        public ICustomerAppService _customerAppService { get; set; }
        public EditModalModel(ISiteSettingAppService siteSettingAppService,
            ICustomerAppService customerAppService) : base(siteSettingAppService)
        {
            _customerAppService = customerAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync(Guid id)
        {
            await CheckAll();
            customerEditModal = ObjectMapper.Map<CustomerDto, CustomerEditModal>(
                    await _customerAppService.GetAsync(id)
                );
            return await Task.FromResult<IActionResult>(Page());
        }
        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            customerEditModal.TenantId = CurrentTenant.Id;
            var update = ObjectMapper.Map<CustomerCreateModal, CustomerCreateUpdateDto>(customerEditModal);
            await _customerAppService.UpdateAsync(customerEditModal.Id, update);
            return NoContent();
        }
    }
}
