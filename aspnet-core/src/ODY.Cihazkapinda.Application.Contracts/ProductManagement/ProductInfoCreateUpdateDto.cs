using System;
using System.Collections.Generic;
using System.Text;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductInfoCreateUpdateDto
    {
        public Guid? TenantId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductInfoTemplateId { get; set; }
    }
}
