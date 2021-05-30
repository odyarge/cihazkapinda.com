using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.GeneralSettings
{
    public interface IGeneralSettingAppService : ICrudAppService<GeneralSettingDto, Guid, PagedAndSortedResultRequestDto, GeneralSettingCreateUpdateDto, GeneralSettingCreateUpdateDto>
    {
        Task<string> GetAsyncTheme(Guid? input);
        Task<GeneralSettingDto> GetAsyncByTenant(Guid? input);
    }
}
