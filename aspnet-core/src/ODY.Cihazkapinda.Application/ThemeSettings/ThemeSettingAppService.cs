using Microsoft.AspNetCore.Authorization;
using ODY.Cihazkapinda.GeneralSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
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
        private readonly IGeneralSettingAppService _generalSettingAppService;
        public ThemeSettingAppService(IRepository<ThemeSetting, Guid> repository, IDataFilter dataFilter, IGeneralSettingAppService generalSettingAppService) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.ThemeSettingDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.ThemeSettings.Delete;

            _dataFilter = dataFilter;
            _generalSettingAppService = generalSettingAppService;
        }

        [RemoteService(false)]
        [Authorize(Permissions.CihazkapindaPermissions.ThemeSettings.List)]
        public async Task<List<ThemeSettingDto>> GetListAsyncAllThemes()
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                var item = await Repository.GetListAsync();
                return ObjectMapper.Map<List<ThemeSetting>, List<ThemeSettingDto>>(item);
            }
        }

        [RemoteService(false)]
        [Authorize(Permissions.CihazkapindaPermissions.ThemeSettings.ThemeSettingDefault)]
        public async Task<bool> GetAsyncProThemeControl(Guid? id)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                var theme = await _generalSettingAppService.GetAsyncByTenant(id);
                var item = await Repository.FindAsync(x => x.THEME_NAME == theme.SiteTheme);

                return item.THEME_PRO;
            }
        }
    }
}
