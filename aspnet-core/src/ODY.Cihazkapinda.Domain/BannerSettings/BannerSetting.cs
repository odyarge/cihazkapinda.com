using JetBrains.Annotations;
using ODY.Cihazkapinda.BannerImages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.BannerSettings
{
    public class BannerSetting : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }

        public string Title { get; set; }
        public bool Active { get; set; }
        public ICollection<BannerImage> Images { get; set; }

        protected BannerSetting()
        {

        }
        public BannerSetting(Guid? _tenantId,
            [NotNull] string _title,
            bool _active)
        {
            TenantId = _tenantId;
            Title = Check.NotNullOrWhiteSpace(_title, nameof(_title));
            Active = _active;
            Images = new Collection<BannerImage>();
        }
    }
}
