using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.ProductManagementModals
{
    public class ProductPropertyTitleCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }
    }
}
