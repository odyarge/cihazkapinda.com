using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.ThemeSettings
{
    public class ThemeSetting : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string THEME_NAME { get; set; }
        public string THEME_PATH { get; set; }
        public bool THEME_ACTIVATED { get; set; }

        protected ThemeSetting()
        {

        }
        public ThemeSetting(Guid? tenantId,
            [NotNull] string _themeName,
            [NotNull] string _themePath,
            bool _themeActivated)
        {
            TenantId = tenantId;
            THEME_NAME = _themeName;
            THEME_PATH = _themePath;
            THEME_ACTIVATED = _themeActivated;
        }
    }
}
