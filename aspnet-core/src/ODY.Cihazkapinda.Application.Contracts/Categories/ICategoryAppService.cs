using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.Categories
{
    public interface ICategoryAppService : ICrudAppService<CategoryDto, Guid, PagedAndSortedResultRequestDto, CategoryCreateUpdateDto, CategoryCreateUpdateDto>
    {
        Task<List<CategoryDto>> GetAllList();
    }
}
