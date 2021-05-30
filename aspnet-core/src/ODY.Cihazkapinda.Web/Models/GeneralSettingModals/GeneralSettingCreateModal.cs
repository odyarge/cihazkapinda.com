using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.GeneralSettingModals
{
    public class GeneralSettingCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }
        public string Logo { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "WorkTime")]
        public string WorkTime { get; set; }
        [Display(Name = "SiteTheme")]
        public string SiteTheme { get; set; }
    }
}
