using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.BannerSettings
{
    public interface IBannerSettingAppService : ICrudAppService<BannerSettingDto, Guid, PagedAndSortedResultRequestDto, BannerSettingCreateUpdateDto, BannerSettingCreateUpdateDto>
    {
    }
}
