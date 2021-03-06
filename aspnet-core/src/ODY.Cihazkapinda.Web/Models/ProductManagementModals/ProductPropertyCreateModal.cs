using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.ProductManagementModals
{
    public class ProductPropertyCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }

        [Required]
        [Display(Name = "Key")]
        public string KEY { get; set; }

        [Required]
        [Display(Name = "Value")]
        public string VALUE { get; set; }

        [Required]
        [Display(Name = "ProductId")]
        public Guid ProductId { get; set; }
    }
}
