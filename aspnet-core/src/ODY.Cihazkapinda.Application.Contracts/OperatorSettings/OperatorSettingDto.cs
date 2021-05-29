using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.OperatorSettings
{
    public class OperatorSettingDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; protected set; }
        public string OperatorName { get; set; }
        public string Image { get; set; }
    }
}
