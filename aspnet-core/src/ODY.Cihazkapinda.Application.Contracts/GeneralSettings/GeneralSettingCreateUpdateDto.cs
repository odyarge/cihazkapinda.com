using System;
using System.Collections.Generic;
using System.Text;

namespace ODY.Cihazkapinda.GeneralSettings
{
    public class GeneralSettingCreateUpdateDto
    {
        public Guid? TenantId { get; protected set; }
        public string Logo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string WorkTime { get; set; }
    }
}
