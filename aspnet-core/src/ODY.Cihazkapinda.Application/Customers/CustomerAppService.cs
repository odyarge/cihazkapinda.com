using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ODY.Cihazkapinda.Customers
{
    public class CustomerAppService : CrudAppService<Customer, CustomerDto, Guid, PagedAndSortedResultRequestDto, CustomerCreateUpdateDto, CustomerCreateUpdateDto>, ICustomerAppService
    {
        public CustomerAppService(IRepository<Customer, Guid> repository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.Customers.CustomersDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.Customers.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.Customers.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.Customers.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.Customers.Delete;
        }

        public async Task<CustomerDto> GetCustomerByPhoneAndUsername(string username, string phone)
        {
            var customer = await Repository.SingleOrDefaultAsync(x => x.NameAndSurname == username && x.Phone == phone);
            return customer != null ? ObjectMapper.Map<Customer, CustomerDto>(customer) : null;
        }
    }
}
