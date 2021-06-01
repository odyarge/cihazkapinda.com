using System;
using System.Collections.Generic;
using System.Text;

namespace ODY.Cihazkapinda.Categories
{
    public class CategoryCreateUpdateDto
    {
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
        public Guid? SubCategory { get; set; }
        public bool Active { get; set; }
    }
}
