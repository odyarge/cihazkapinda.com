using ODY.Cihazkapinda.Categories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int Installment { get; set; }
        public bool Discount { get; set; }
        public bool Active { get; set; }
        public ICollection<ProductImageDto> Images { get; set; }
        public ProductPropertyDto ProductProperty { get; set; }
        public CategoryDto Category { get; set; }

        public ProductDto()
        {
            Images = new Collection<ProductImageDto>();
            ProductProperty = new ProductPropertyDto();
            Category = new CategoryDto();
        }
    }
}
