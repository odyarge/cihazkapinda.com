using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductProperty : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string KEY { get; set; }
        public string VALUE { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }

        protected ProductProperty()
        {

        }
        public ProductProperty(Guid? tenantId,
            [NotNull] string _key,
            [NotNull] string _value,
            Guid _productId)
        {
            TenantId = tenantId;
            KEY = Check.NotNullOrWhiteSpace(_key, nameof(_key));
            VALUE = Check.NotNullOrWhiteSpace(_value, nameof(_value));
            ProductId = _productId;
        }
    }
}
