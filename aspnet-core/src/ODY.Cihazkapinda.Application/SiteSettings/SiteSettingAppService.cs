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

namespace ODY.Cihazkapinda.SiteSettings
{
    public class SiteSettingAppService : CrudAppService<SiteSetting, SiteSettingDto, Guid, PagedAndSortedResultRequestDto, SiteSettingCreateUpdateDto, SiteSettingCreateUpdateDto>, ISiteSettingAppService
    {
        private readonly IDataFilter _dataFilter;
        public SiteSettingAppService(IRepository<SiteSetting, Guid> repository, IDataFilter dataFilter) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.SiteSettings.SiteSettingDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.SiteSettings.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.SiteSettings.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.SiteSettings.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.SiteSettings.Delete;

            _dataFilter = dataFilter;
        }

        [RemoteService(false)]
        public async Task<SiteSettingDto> GetAsyncByTenantName(string input)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                var item = await Repository.FindAsync(x => x.SITE_OWNER == input);
                return ObjectMapper.Map<SiteSetting, SiteSettingDto>(item);
            }
        }

        [RemoteService(false)]
        public async Task<bool> UpdateInstall(string license, string owner)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                var item = await Repository.FindAsync(x => x.SITE_OWNER == owner && x.SITE_LICENSE == license);
                item.SITE_INSTALL = true;
                await Repository.UpdateAsync(item);
                return true;
            }
        }
    }
}
