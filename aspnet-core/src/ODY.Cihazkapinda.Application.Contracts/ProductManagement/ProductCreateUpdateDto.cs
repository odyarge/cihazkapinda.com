using System;
using System.Collections.Generic;
using System.Text;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductCreateUpdateDto
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
        public Guid CategoryId { get; set; }
    }
}
