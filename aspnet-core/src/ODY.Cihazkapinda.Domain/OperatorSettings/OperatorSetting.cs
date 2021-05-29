using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.OperatorSettings
{
    public class OperatorSetting : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string OperatorName { get; set; }
        public string Image { get; set; }
        protected OperatorSetting()
        {

        }
        public OperatorSetting(Guid? tenantId,
            [NotNull] string _operatorName,
            [NotNull] string _image)
        {
            TenantId = tenantId;
            OperatorName = _operatorName;
            Image = _image;
        }
    }
}
