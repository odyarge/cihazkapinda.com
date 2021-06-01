using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.ThemeSettings
{
    public class ThemeSettingDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; protected set; }
        public string THEME_NAME { get; set; }
        public string THEME_PATH { get; set; }
        public bool THEME_ACTIVATED { get; set; }
        public bool THEME_PRO { get; set; }
    }
}
