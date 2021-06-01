using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.BannerImages
{
    public class BannerImageDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; set; }
        public Guid BannerSettingId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
    }
}
