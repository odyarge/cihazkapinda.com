using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.BannerSettings
{
    public interface IBannerSettingAppService : ICrudAppService<BannerSettingDto, Guid, PagedAndSortedResultRequestDto, BannerSettingCreateUpdateDto, BannerSettingCreateUpdateDto>
    {
        Task<long> GetAsyncBannerCount(Guid? tenantId);
        Task<BannerSettingDto> GetAsyncBannerWithImage(Guid Id);
    }
}
