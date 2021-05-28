using System;
using System.Collections.Generic;
using System.Text;

namespace ODY.Cihazkapinda.BannerImages
{
    public class BannerImageCreateUpdateDto
    {
        public Guid? TenantId { get; protected set; }
        public string Image { get; set; }
    }
}
