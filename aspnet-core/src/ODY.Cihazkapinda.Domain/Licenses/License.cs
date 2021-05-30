using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.Licenses
{
    public class License : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string LICENSE_OWNER { get; set; }
        public string LICENSE { get; set; }

        protected License()
        {

        }
        public License(Guid? tenantId,
            [NotNull] string _license,
            [NotNull] string _licenseOwner)
        {
            TenantId = tenantId;
            LICENSE = Check.NotNullOrWhiteSpace(_license, nameof(_license));
            LICENSE_OWNER = Check.NotNullOrWhiteSpace(_licenseOwner, nameof(_licenseOwner));
        }
    }
}
