using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.SiteSettings
{
    public class SiteSetting : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string SITE_OWNER { get; set; }
        public string SITE_NAME { get; set; }
        public string SITE_LICENSE { get; set; }
        public string SITE_OPERATOR { get; set; }
        public bool SITE_ACTIVATED { get; set; }
        public bool SITE_INSTALL { get; set; }
        public DateTime SITE_LICENSE_FINISH_TIME { get; set; }

        protected SiteSetting()
        {

        }
        public SiteSetting(Guid? tenantId,
            [NotNull] string _siteOwner,
            [NotNull] string _siteName,
            [NotNull] string _siteLicense,
            [NotNull] string _siteOperator,
            bool _siteActivated,
            bool _siteInstall,
            DateTime _siteLicenseFinishTime)
        {
            TenantId = tenantId;
            SITE_OWNER = Check.NotNullOrWhiteSpace(_siteOwner, nameof(_siteOwner));
            SITE_NAME = Check.NotNullOrWhiteSpace(_siteName, nameof(_siteName));
            SITE_LICENSE = Check.NotNullOrWhiteSpace(_siteLicense, nameof(_siteLicense));
            SITE_OPERATOR = Check.NotNullOrWhiteSpace(_siteOperator, nameof(_siteOperator));
            SITE_ACTIVATED = _siteActivated;
            SITE_INSTALL = _siteInstall;
            SITE_LICENSE_FINISH_TIME = _siteLicenseFinishTime;
        }
    }
}
