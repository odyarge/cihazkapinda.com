﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.ProductManagementModals
{
    public class ProductPropertyTemplateCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
    }
}
