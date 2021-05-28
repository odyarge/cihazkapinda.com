using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.GeneralSettings
{
    public interface IGeneralSettingAppService : ICrudAppService<GeneralSettingDto, Guid, PagedAndSortedResultRequestDto, GeneralSettingCreateUpdateDto, GeneralSettingCreateUpdateDto>
    {
    }
}
