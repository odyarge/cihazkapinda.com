using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductPropertyTitle : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        protected ProductPropertyTitle()
        {

        }
        public ProductPropertyTitle(Guid? tenantId,
            [NotNull] string _name,
            bool _active)
        {
            TenantId = tenantId;
            Name = _name;
            Active = _active;
        }
    }
}
