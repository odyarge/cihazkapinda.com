using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductInfoTemplate : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        protected ProductInfoTemplate()
        {

        }
        public ProductInfoTemplate(Guid? tenantId,
            [NotNull] string _image,
            [NotNull] string _title,
            [NotNull] string _description)
        {
            TenantId = tenantId;
            Image = Check.NotNullOrWhiteSpace(_image, nameof(_image));
            Title = Check.NotNullOrWhiteSpace(_title, nameof(_title));
            Description = Check.NotNullOrWhiteSpace(_description, nameof(_description));
        }
    }
}
