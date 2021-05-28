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

        protected GeneralSetting()
        {

        }

        public GeneralSetting(Guid? _tenantId,
            [NotNull] string _logo,
            [NotNull] string _description,
            [NotNull] string _title,
            string _email,
            string _phone,
            string _workTime)
        {
            TenantId = _tenantId;
            Logo = Check.NotNullOrWhiteSpace(_logo, nameof(_logo));
            Title = Check.NotNullOrWhiteSpace(_title, nameof(_title));
            Description = Check.NotNullOrWhiteSpace(_description, nameof(_description));
            Email = _email;
            Phone = _phone;
            WorkTime = _workTime;
        }
    }
}
