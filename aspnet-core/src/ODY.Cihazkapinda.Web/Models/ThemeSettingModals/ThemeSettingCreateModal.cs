using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.ThemeSettingModals
{
    public class ThemeSettingCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }
        [Required]
        [Display(Name = "ThemeName")]
        public string THEME_NAME { get; set; }
        [Display(Name = "ThemePath")]
        public string THEME_PATH { get; set; }
        [Display(Name = "ThemeActivated")]
        public bool THEME_ACTIVATED { get; set; }
    }
}
