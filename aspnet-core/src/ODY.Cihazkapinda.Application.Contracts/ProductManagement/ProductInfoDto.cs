using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductInfoDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; protected set; }
        public Guid ProductInfoTemplateId { get; set; }
        public Guid ProductId { get; set; }
        public bool Active { get; set; }
    }
}
