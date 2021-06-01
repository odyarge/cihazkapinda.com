using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.BannerImageModals
{
    public class BannerImageCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        public Guid BannerSettingId { get; set; }
        [Required]
        [Display(Name = "Image")]
        public string Image { get; set; }
    }
}
