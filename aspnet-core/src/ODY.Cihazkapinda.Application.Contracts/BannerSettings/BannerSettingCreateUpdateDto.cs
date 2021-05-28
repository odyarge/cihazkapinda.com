using ODY.Cihazkapinda.BannerImages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODY.Cihazkapinda.BannerSettings
{
    public class BannerSettingCreateUpdateDto
    {
        public Guid? TenantId { get; protected set; }

        public string Title { get; set; }
        public string WelcomeMessage { get; set; }
        public ICollection<BannerImageDto> Images { get; set; }
    }
}
