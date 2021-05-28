using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ODY.Cihazkapinda.SiteSettings
{
    public class SiteSettingAppService : CrudAppService<SiteSetting, SiteSettingDto, Guid, PagedAndSortedResultRequestDto, SiteSettingCreateUpdateDto, SiteSettingCreateUpdateDto>, ISiteSettingAppService
    {
        public SiteSettingAppService(IRepository<SiteSetting, Guid> repository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.SiteSettings.SiteSettingDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.SiteSettings.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.SiteSettings.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.SiteSettings.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.SiteSettings.Delete;
        }
    }
}
