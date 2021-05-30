using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.ThemeSettings
{
    public class ThemeSettingAppService : CrudAppService<ThemeSetting, ThemeSettingDto, Guid, PagedAndSortedResultRequestDto, ThemeSettingCreateUpdateDto, ThemeSettingCreateUpdateDto>, IThemeSettingAppService
    {
        private readonly IDataFilter _dataFilter;
        public ThemeSettingAppService(IRepository<ThemeSetting, Guid> repository, IDataFilter dataFilter) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.ThemeSettingDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.Delete;

            _dataFilter = dataFilter;
        }

        public override Task<PagedResultDto<ThemeSettingDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                return base.GetListAsync(input);
            }
        }

    }
}
