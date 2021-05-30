using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.Licenses
{
    public class LicenseDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; protected set; }
        public string LICENSE_OWNER { get; set; }
        public string LICENSE { get; set; }
    }
}
