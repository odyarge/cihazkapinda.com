using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductPropertyDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; protected set; }
        public string KEY { get; set; }
        public string VALUE { get; set; }
    }
}
