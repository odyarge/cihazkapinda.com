using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.SiteSettings
{
    public interface ISiteSettingAppService : ICrudAppService<SiteSettingDto, Guid, PagedAndSortedResultRequestDto, SiteSettingCreateUpdateDto, SiteSettingCreateUpdateDto>
    {
    }
}
