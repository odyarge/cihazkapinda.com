using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.ProductManagement
{
    public interface IProductPropertyTemplateAppService : ICrudAppService<ProductPropertyTemplateDto, Guid, PagedAndSortedResultRequestDto, ProductPropertyTemplateCreateUpdateDto, ProductPropertyTemplateCreateUpdateDto>
    {
        Task<List<ProductPropertyTemplateDto>> GetAllList();
        Task<ProductPropertyTemplateDto> GetAsyncProductTemplateWithSub(Guid Id);
    }
}
