using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.ProductManagement
{
    public interface IProductPropertyTitleAppService : ICrudAppService<ProductPropertyTitleDto, Guid, PagedAndSortedResultRequestDto, ProductPropertyTitleCreateUpdateDto, ProductPropertyTitleCreateUpdateDto>
    {
        Task<List<ProductPropertyTitleDto>> GetAllList();
    }
}
