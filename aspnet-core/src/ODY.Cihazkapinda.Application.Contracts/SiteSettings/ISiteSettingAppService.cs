using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.SiteSettings
{
    public interface ISiteSettingAppService : ICrudAppService<SiteSettingDto, Guid, PagedAndSortedResultRequestDto, SiteSettingCreateUpdateDto, SiteSettingCreateUpdateDto>
    {
        Task<SiteSettingDto> GetAsyncByTenantName(string input);
    }
}
