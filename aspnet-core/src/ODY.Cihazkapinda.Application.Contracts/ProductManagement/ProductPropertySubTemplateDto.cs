using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class ProductPropertySubTemplateDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; protected set; }
        [Display(Name = "Key")]
        public string KEY { get; set; }
        [Display(Name = "Value")]
        public string VALUE { get; set; }
        public string TITLE { get; set; }
    }
}
