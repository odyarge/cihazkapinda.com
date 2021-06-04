using Microsoft.AspNetCore.Mvc;
using ODY.Cihazkapinda.ProductManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.ProductManagementModals
{
    public class ProductPropertyTemplateEditModal : ProductPropertyTemplateCreateModal
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [NotMapped]
        public ICollection<ProductPropertySubTemplateDto> ProductPropertySubTemplates { get; set; }
    }
}
