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
    public class ProductImage : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string Image { get; set; }

        public Product Product { get; set; }
        public Guid ProductId { get; set; }

        protected ProductImage()
        {

        }
        public ProductImage(Guid? tenantId,
            [NotNull] string _image,
            Guid _productId)
        {
            TenantId = tenantId;
            Image = _image;
            ProductId = _productId;
        }
    }
}
