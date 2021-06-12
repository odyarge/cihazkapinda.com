using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ProductInfoTemplate productInfoTemplate { get; set; }
        public Guid ProductId { get; set; }

        protected ProductInfo()
        {

        }
        public ProductInfo(Guid? tenantId,
            Guid _productId,
            Guid _productInfoTemplateId)
        {
            TenantId = tenantId;
            ProductId = _productId;
            ProductInfoTemplateId = _productInfoTemplateId;
        }
    }
}
