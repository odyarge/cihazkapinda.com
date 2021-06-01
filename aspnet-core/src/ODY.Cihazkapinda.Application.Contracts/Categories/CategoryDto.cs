using ODY.Cihazkapinda.ProductManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.Categories
{
    public class CategoryDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; protected set; }
        public string Name { get; set; }
        public Guid? SubCategory { get; set; }
        public bool Active { get; set; }
        public ICollection<ProductDto> Products { get; set; }

        public CategoryDto()
        {
            Products = new Collection<ProductDto>();
        }
    }
}
