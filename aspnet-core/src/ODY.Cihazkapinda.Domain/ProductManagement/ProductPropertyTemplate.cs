using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductPropertyTemplate : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string Title { get; set; }
        public ICollection<ProductPropertySubTemplate> ProductPropertySubTemplates { get; set; }

        protected ProductPropertyTemplate()
        {

        }
        public ProductPropertyTemplate(Guid? tenantId,
            [NotNull] string _title)
        {
            TenantId = tenantId;
            Title = _title;
            ProductPropertySubTemplates = new Collection<ProductPropertySubTemplate>();
        }
    }
}
