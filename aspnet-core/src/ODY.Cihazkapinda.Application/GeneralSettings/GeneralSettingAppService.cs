using Microsoft.AspNetCore.Authorization;
using ODY.Cihazkapinda.ThemeSettings;
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

namespace ODY.Cihazkapinda.GeneralSettings
{
    public class GeneralSettingAppService : CrudAppService<GeneralSetting, GeneralSettingDto, Guid, PagedAndSortedResultRequestDto, GeneralSettingCreateUpdateDto, GeneralSettingCreateUpdateDto>, IGeneralSettingAppService
    {
        private readonly IRepository<ThemeSetting, Guid> _themeRepository;
        private readonly IDataFilter _dataFilter;
        public GeneralSettingAppService(IRepository<GeneralSetting, Guid> repository, IRepository<ThemeSetting, Guid> themeRepository, IDataFilter dataFilter) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.GeneralSettings.GeneralSettingDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.GeneralSettings.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.GeneralSettings.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.GeneralSettings.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.GeneralSettings.Delete;

            _themeRepository = themeRepository;
            _dataFilter = dataFilter;
        }

        [Authorize(Permissions.CihazkapindaPermissions.BannerSettings.List)]
        public async Task<List<GeneralSettingDto>> GetAllList()
        {
            var list = await Repository.GetListAsync();
            return ObjectMapper.Map<List<GeneralSetting>, List<GeneralSettingDto>>(list.OrderByDescending(x => x.CreationTime).ToList());
        }

        [RemoteService(false)]
        public async Task<string> GetAsyncTheme(Guid? input)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                var item = await Repository.FindAsync(x => x.TenantId == input);
                var theme = await _themeRepository.FindAsync(x => x.THEME_NAME == item.SiteTheme);
                return theme.THEME_PATH;
            }

        }
        [RemoteService(false)]
        public async Task<GeneralSettingDto> GetAsyncByTenant(Guid? input)
        {
            var item = await Repository.FindAsync(x => x.TenantId == input);
            return ObjectMapper.Map<GeneralSetting, GeneralSettingDto>(item);
        }
    }
}
