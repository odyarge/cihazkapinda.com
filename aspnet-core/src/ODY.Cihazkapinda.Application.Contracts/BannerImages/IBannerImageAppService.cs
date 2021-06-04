using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.BannerImages
{
    public interface IBannerImageAppService : ICrudAppService<BannerImageDto, Guid, PagedAndSortedResultRequestDto, BannerImageCreateUpdateDto, BannerImageCreateUpdateDto>
    {
        Task<List<BannerImageDto>> GetAllList();
    }
}
