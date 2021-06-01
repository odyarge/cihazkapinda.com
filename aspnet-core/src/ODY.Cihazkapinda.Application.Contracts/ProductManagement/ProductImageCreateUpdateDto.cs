using System;
using System.Collections.Generic;
using System.Text;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductImageCreateUpdateDto
    {
        public Guid? TenantId { get; set; }
        public string Image { get; set; }
        public Guid ProductId { get; set; }
    }
}
