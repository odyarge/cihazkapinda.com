using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductInfoTemplateDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; protected set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
