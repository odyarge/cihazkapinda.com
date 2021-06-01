using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.ProductManagementModals
{
    public class ProductImageCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }

        [Required]
        [Display(Name = "Image")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "ProductId")]
        public Guid ProductId { get; set; }
    }
}
