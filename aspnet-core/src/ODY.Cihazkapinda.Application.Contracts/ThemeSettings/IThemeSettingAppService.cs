using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.ThemeSettings
{
    public interface IThemeSettingAppService : ICrudAppService<ThemeSettingDto, Guid, PagedAndSortedResultRequestDto, ThemeSettingCreateUpdateDto, ThemeSettingCreateUpdateDto>
    {
    }
}
