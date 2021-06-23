using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.MessageData
{
    public class Messages : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string Sender { get; set; }
        public string Receive { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }

        protected Messages()
        {

        }
        public Messages(Guid? tenantId,
            [NotNull] string _sender,
            [NotNull] string _receive,
            [NotNull] string _message,
            int _status
            )
        {
            TenantId = tenantId;
            Sender = _sender;
            Receive = _receive;
            Message = _message;
            Status = _status;
        }
    }
}
