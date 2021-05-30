using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ODY.Cihazkapinda.SiteSettings
{
    public class SiteSettingCreateUpdateDto
    {
        public Guid? TenantId { get; protected set; }
        [Required]
        public string SITE_OWNER { get; set; }
        [Required]
        public string SITE_NAME { get; set; }
        [Required]
        public string SITE_LICENSE { get; set; }
        [Required]
        public string SITE_OPERATOR { get; set; }
        [Required]
        public bool SITE_ACTIVATED { get; set; }
        [Required]
        public bool SITE_INSTALL { get; set; }
        public DateTime SITE_LICENSE_FINISH_TIME { get; set; }
    }
}
