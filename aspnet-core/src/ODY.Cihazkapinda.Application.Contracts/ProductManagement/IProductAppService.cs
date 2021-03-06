using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.ProductManagement
{
    public interface IProductAppService : ICrudAppService<ProductDto, Guid, PagedAndSortedResultRequestDto, ProductCreateUpdateDto, ProductCreateUpdateDto>
    {
        Task<List<ProductDto>> GetAllList();
        Task<ProductDto> GetAsyncProductWithDetail(Guid id);
        Task<PagedResultDto<ProductDto>> GetAsyncProductWithImageList(ProductSearchDto input);
    }
}
