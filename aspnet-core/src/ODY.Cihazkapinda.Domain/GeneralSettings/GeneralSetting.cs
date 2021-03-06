using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.GeneralSettings
{
    public class GeneralSetting : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string Logo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string WorkTime { get; set; }
        public string SiteTheme { get; set; }
        public string City { get; set; }

        protected GeneralSetting()
        {

        }

        public GeneralSetting(Guid? _tenantId,
            [NotNull] string _logo,
            [NotNull] string _description,
            [NotNull] string _title,
            [NotNull] string _siteTheme,
            [NotNull] string _city,
            string _email,
            string _phone,
            string _workTime)
        {
            TenantId = _tenantId;
            Logo = Check.NotNullOrWhiteSpace(_logo, nameof(_logo));
            Title = Check.NotNullOrWhiteSpace(_title, nameof(_title));
            Description = Check.NotNullOrWhiteSpace(_description, nameof(_description));
            SiteTheme = Check.NotNullOrWhiteSpace(_siteTheme, nameof(_siteTheme));
            City = Check.NotNullOrWhiteSpace(_city, nameof(_city));
            Email = _email;
            Phone = _phone;
            WorkTime = _workTime;
        }
    }
}
