using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.ObjectExtending;

namespace ODY.Cihazkapinda.Web.Models.SiteSettingModals
{
    public class SiteSettingEditModal : SiteSettingCreateModal
    {
        [HiddenInput]
        public Guid Id { get; set; }
    }
}
