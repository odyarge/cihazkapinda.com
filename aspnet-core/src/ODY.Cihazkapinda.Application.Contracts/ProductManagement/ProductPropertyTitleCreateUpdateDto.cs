using System;
using System.Collections.Generic;
using System.Text;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductPropertyTitleCreateUpdateDto
    {
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
