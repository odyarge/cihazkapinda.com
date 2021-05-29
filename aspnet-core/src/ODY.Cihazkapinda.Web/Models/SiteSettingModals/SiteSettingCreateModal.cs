using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.ObjectExtending;

namespace ODY.Cihazkapinda.Web.Models.SiteSettingModals
{
    public class SiteSettingCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }
        [Required]
        [Display(Name = "SiteOwner")]
        public string SITE_OWNER { get; set; }
        [Required]
        [Display(Name = "SiteName")]
        public string SITE_NAME { get; set; }
        [Required]
        [Display(Name = "SiteLicense")]
        public string SITE_LICENSE { get; set; }
        [Required]
        [Display(Name = "SiteOperator")]
        public string SITE_OPERATOR { get; set; }
        [Required]
        [Display(Name = "SiteActivated")]
        public bool SITE_ACTIVATED { get; set; }
        [Required]
        [Display(Name = "SiteLicenseFinishTime")]
        public DateTime SITE_LICENSE_FINISH_TIME { get; set; }
    }
}
