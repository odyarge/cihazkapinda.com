using Microsoft.AspNetCore.Mvc;
using ODY.Cihazkapinda.BannerImages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.BannerSettingModals
{
    public class BannerSettingCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Active")]
        public bool Active { get; set; }
    }
}
