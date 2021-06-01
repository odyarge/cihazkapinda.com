using JetBrains.Annotations;
using ODY.Cihazkapinda.BannerSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.BannerImages
{
    public class BannerImage : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public BannerSetting BannerSetting { get; set; }
        public Guid BannerSettingId { get; set; }


        protected BannerImage()
        {

        }
        public BannerImage(Guid? _tenantId,
            Guid _bannerSettingId,
            [NotNull] string _title,
            [NotNull] string _content,
            [NotNull] string _image)
        {
            TenantId = _tenantId;
            BannerSettingId = _bannerSettingId;
            Content = Check.NotNullOrWhiteSpace(_content, nameof(_content));
            Title = Check.NotNullOrWhiteSpace(_title, nameof(_title));
            Image = Check.NotNullOrWhiteSpace(_image, nameof(_image));
        }
    }
}
