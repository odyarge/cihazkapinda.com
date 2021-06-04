using System;
using System.Collections.Generic;
using System.Text;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductPropertyTemplateCreateUpdateDto
    {
        public Guid? TenantId { get; set; }
        public string Title { get; set; }
    }
}
