using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductInfo : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public Guid ProductInfoTemplateId { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public bool Active { get; set; }

        [NotMapped]
        public ProductInfoTemplate ProductInfoTemplate { get; set; }

        protected ProductInfo()
        {

        }
        public ProductInfo(Guid? tenantId,
            Guid productId,
            Guid productInfoTemplateId,
            bool _active)
        {
            TenantId = tenantId;
            ProductId = productId;
            ProductInfoTemplateId = productInfoTemplateId;
            Active = _active;
        }
    }
}
