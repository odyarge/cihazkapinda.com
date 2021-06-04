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
    public class ProductPropertySubTemplate : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string KEY { get; set; }
        public string VALUE { get; set; }
        public string TITLE { get; set; }
        public ProductPropertyTemplate ProductPropertyTemplate { get; set; }
        public Guid ProductPropertyTemplateId { get; set; }

        protected ProductPropertySubTemplate()
        {

        }
        public ProductPropertySubTemplate(Guid? tenantId,
            [NotNull] string _key,
            [NotNull] string _value,
            [NotNull] string _title,
            Guid _productPropertyTemplateId)
        {
            TenantId = tenantId;
            KEY = Check.NotNullOrWhiteSpace(_key, nameof(_key));
            VALUE = Check.NotNullOrWhiteSpace(_value, nameof(_value));
            TITLE = Check.NotNullOrWhiteSpace(_title, nameof(_title));
            ProductPropertyTemplateId = _productPropertyTemplateId;
        }
    }
}
