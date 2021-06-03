using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductPropertyTitleDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
