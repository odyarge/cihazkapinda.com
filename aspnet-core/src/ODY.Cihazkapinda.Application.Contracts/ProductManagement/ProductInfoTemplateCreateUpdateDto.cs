using System;
using System.Collections.Generic;
using System.Text;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductInfoTemplateCreateUpdateDto
    {
        public Guid? TenantId { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
