using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.ProductManagement
{
    public interface IProductInfoAppService : ICrudAppService<ProductInfoDto, Guid, PagedAndSortedResultRequestDto, ProductInfoCreateUpdateDto, ProductInfoCreateUpdateDto>
    {
        Task<List<ProductInfoDto>> GetAllList(); 
        Task<List<ProductInfoDto>> GetAllListWithDetail(Guid id);
        Task<bool> GetControl(Guid id);
    }
}
