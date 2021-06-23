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
    public class CreateModalModel : CihazkapindaPageModel
    {
        [BindProperty]
        public CustomerCreateModal customerCreateModal { get; set; }

        public ICustomerAppService _customerAppService { get; set; }
        public CreateModalModel(ISiteSettingAppService siteSettingAppService,
            ICustomerAppService customerAppService) : base(siteSettingAppService)
        {
            _customerAppService = customerAppService;
        }
        public async virtual Task<IActionResult> OnGetAsync(string input)
        {
            await CheckAll();
            if (string.IsNullOrEmpty(input))
            {
                customerCreateModal = new CustomerCreateModal();
            }
            else
            {
                string phone = input.Substring(0, input.IndexOf('_'));
                int startIndex = input.IndexOf('_') + 1;
                int endIndex = input.Length;
                int length = endIndex - startIndex;
                string name = input.Substring(startIndex, length);
                customerCreateModal = new CustomerCreateModal() { Name = name, Phone = phone };
            }
            return await Task.FromResult<IActionResult>(Page());
        }
        public virtual async Task<IActionResult> OnPostAsync()
        {
            ValidateModel();

            customerCreateModal.NameAndSurname = customerCreateModal.Name + customerCreateModal.Surname;
            customerCreateModal.TenantId = CurrentTenant.Id;
            var create = ObjectMapper.Map<CustomerCreateModal, CustomerCreateUpdateDto>(customerCreateModal);
            await _customerAppService.CreateAsync(create);
            return NoContent();
        }
    }
}
