using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.LicenseModals
{
    public class LicenseCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }
        [Required]
        [Display(Name = "LicenseOwner")]
        public string LICENSE_OWNER { get; set; }
        [Required]
        [Display(Name = "License")]
        public string LICENSE { get; set; }
    }
}
