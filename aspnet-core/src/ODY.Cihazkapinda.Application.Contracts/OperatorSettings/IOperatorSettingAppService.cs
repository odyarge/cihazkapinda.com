using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ODY.Cihazkapinda.OperatorSettings
{
    public interface IOperatorSettingAppService : ICrudAppService<OperatorSettingDto, Guid, PagedAndSortedResultRequestDto, OperatorSettingCreateUpdateDto, OperatorSettingCreateUpdateDto>
    {
        Task<List<OperatorSettingDto>> GetAllList();
    }
}
