using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.LicenseModals
{
    public class LicenseCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }
        public string LICENSE_OWNER { get; set; }
        public string LICENSE { get; set; }
    }
}
