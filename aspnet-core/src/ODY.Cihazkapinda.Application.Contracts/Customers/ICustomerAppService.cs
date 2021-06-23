using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.Customers
{
    public interface ICustomerAppService : ICrudAppService<CustomerDto, Guid, PagedAndSortedResultRequestDto, CustomerCreateUpdateDto, CustomerCreateUpdateDto>
    {
        Task<CustomerDto> GetCustomerByPhoneAndUsername(string username, string phone);
    }
}
