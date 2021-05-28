using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ODY.Cihazkapinda.BannerSettings
{
    public class BannerSettingAppService : CrudAppService<BannerSetting, BannerSettingDto, Guid, PagedAndSortedResultRequestDto, BannerSettingCreateUpdateDto, BannerSettingCreateUpdateDto>, IBannerSettingAppService
    {
        public BannerSettingAppService(IRepository<BannerSetting, Guid> repository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.BannerSettings.BannerSettingDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.BannerSettings.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.BannerSettings.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.BannerSettings.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.BannerSettings.Delete;
        }
    }
}
