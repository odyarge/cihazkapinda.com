using System;
using System.Collections.Generic;
using System.Text;

namespace ODY.Cihazkapinda.Licenses
{
    public class LicenseCreateUpdateDto
    {
        public Guid? TenantId { get; protected set; }
        public string LICENSE_OWNER { get; set; }
        public string LICENSE { get; set; }
    }
}
