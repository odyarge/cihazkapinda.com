using ODY.Cihazkapinda.BannerImages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ODY.Cihazkapinda.BannerSettings
{
    public class BannerSettingDto : FullAuditedEntityDto<Guid>
    {
        public Guid? TenantId { get; protected set; }

        public string Title { get; set; }
        public ICollection<BannerImageDto> Images { get; set; }
        public bool Active { get; set; }

        public BannerSettingDto()
        {
            Images = new Collection<BannerImageDto>();
        }
    }
}
