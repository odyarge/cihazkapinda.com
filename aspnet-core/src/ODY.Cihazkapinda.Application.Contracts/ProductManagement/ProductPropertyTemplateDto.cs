using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductPropertyTemplateDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; set; }
        public string Title { get; set; }
        public ICollection<ProductPropertySubTemplateDto> ProductPropertySubTemplates { get; set; }

        public ProductPropertyTemplateDto()
        {
            ProductPropertySubTemplates = new Collection<ProductPropertySubTemplateDto>();
        }
    }
}
