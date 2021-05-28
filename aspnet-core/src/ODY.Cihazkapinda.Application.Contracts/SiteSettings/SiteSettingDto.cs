using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.SiteSettings
{
    public class SiteSettingDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; protected set; }
        public string SITE_OWNER { get; set; }
        public string SITE_NAME { get; set; }
        public string SITE_LICENSE { get; set; }
        public string SITE_OPERATOR { get; set; }
        public bool SITE_ACTIVATED { get; set; }
        public DateTime SITE_LICENSE_FINISH_TIME { get; set; }
    }
}
