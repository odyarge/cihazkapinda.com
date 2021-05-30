using Microsoft.AspNetCore.Mvc;
using ODY.Cihazkapinda.Web.Models.GeneralSettingModals;
using ODY.Cihazkapinda.Web.Models.LicenseModals;
using ODY.Cihazkapinda.Web.Models.SiteSettingModals;
using ODY.Cihazkapinda.Web.Models.ThemeSettingModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models
{
    public class InstallModal
    {
        [BindProperty]
        public LicenseCreateModal licenseCreateModal { get; set; }

        [BindProperty]
        public ThemeSettingCreateModal themeSettingCreateModal { get; set; }

        [BindProperty]
        public GeneralSettingCreateModal generalSettingCreateModal { get; set; }
    }
}
