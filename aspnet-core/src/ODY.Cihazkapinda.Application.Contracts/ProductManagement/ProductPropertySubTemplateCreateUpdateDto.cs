using System;
using System.Collections.Generic;
using System.Text;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductPropertySubTemplateCreateUpdateDto
    {
        public Guid? TenantId { get; set; }
        public string KEY { get; set; }
        public string VALUE { get; set; }
        public string TITLE { get; set; }
        public Guid ProductPropertyTemplateId { get; set; }
    }
}
