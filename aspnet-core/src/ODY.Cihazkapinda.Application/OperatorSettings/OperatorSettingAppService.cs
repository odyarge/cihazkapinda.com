using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ODY.Cihazkapinda.OperatorSettings
{
    public class OperatorSettingAppService : CrudAppService<OperatorSetting, OperatorSettingDto, Guid, PagedAndSortedResultRequestDto, OperatorSettingCreateUpdateDto, OperatorSettingCreateUpdateDto>, IOperatorSettingAppService
    {
        public OperatorSettingAppService(IRepository<OperatorSetting, Guid> repository) : base(repository)
        {
            GetPolicyName = Permissions.CihazkapindaPermissions.OperatorSettings.OperatorSettingDefault;
            GetListPolicyName = Permissions.CihazkapindaPermissions.OperatorSettings.List;
            CreatePolicyName = Permissions.CihazkapindaPermissions.OperatorSettings.Create;
            UpdatePolicyName = Permissions.CihazkapindaPermissions.OperatorSettings.Edit;
            DeletePolicyName = Permissions.CihazkapindaPermissions.OperatorSettings.Delete;
        }

        [Authorize(Permissions.CihazkapindaPermissions.OperatorSettings.List)]
        public async Task<List<OperatorSettingDto>> GetAllList()
        {
            var list = await Repository.GetListAsync();
            return ObjectMapper.Map<List<OperatorSetting>, List<OperatorSettingDto>>(list.OrderByDescending(x => x.CreationTime).ToList());
        }
    }
}
