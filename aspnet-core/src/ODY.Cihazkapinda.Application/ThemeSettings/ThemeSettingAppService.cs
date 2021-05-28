using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ODY.Cihazkapinda.ThemeSettings
{
    public class ThemeSettingAppService : CrudAppService<ThemeSetting, ThemeSettingDto, Guid, PagedAndSortedResultRequestDto, ThemeSettingCreateUpdateDto, ThemeSettingCreateUpdateDto>, IThemeSettingAppService
    {
        public ThemeSettingAppService(IRepository<ThemeSetting, Guid> repository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.ThemeSettingDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.Delete;
        }
    }
}
