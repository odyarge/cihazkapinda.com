using Microsoft.AspNetCore.Mvc;
using ODY.Cihazkapinda.BannerImages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.BannerSettingModals
{
    public class BannerSettingEditModal : BannerSettingCreateModal
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [NotMapped]
        public ICollection<BannerImageDto> Images { get; set; }
    }
}
