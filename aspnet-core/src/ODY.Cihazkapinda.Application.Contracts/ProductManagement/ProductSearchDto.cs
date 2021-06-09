using ODY.Cihazkapinda.ProductColorTypes;
using ODY.Cihazkapinda.ProductTypes;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductSearchDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public Guid? CategoryId { get; set; }
        public int? Installment { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public ProductColorType? ProductColor { get; set; }
        public ProductType? ProductType { get; set; }
    }
}
