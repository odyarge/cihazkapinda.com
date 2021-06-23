using System;
using System.Collections.Generic;
using System.Text;

namespace ODY.Cihazkapinda.MessageData
{
    public class MessagesCreateUpdateDto
    {
        public Guid? TenantId { get; set; }
        public string Sender { get; set; }
        public string Receive { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }
}
