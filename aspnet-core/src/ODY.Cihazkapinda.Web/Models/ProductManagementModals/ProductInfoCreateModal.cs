using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.ProductManagementModals
{
    public class ProductInfoCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }

        [HiddenInput]
        public Guid ProductInfoTemplateId { get; set; }

        [HiddenInput]
        public Guid ProductId { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }
    }
}
