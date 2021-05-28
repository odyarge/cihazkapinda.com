using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ODY.Cihazkapinda.ThemeSettings
{
    public class ThemeSettingCreateUpdateDto
    {
        public Guid? TenantId { get; protected set; }
        [Required]
        public string THEME_NAME { get; set; }
        [Required]
        public string THEME_PATH { get; set; }
        public bool THEME_ACTIVATED { get; set; }
    }
}
