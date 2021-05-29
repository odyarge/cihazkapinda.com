using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ODY.Cihazkapinda.OperatorSettings
{
    public class OperatorSettingCreateUpdateDto
    {
        public Guid? TenantId { get; protected set; }
        [Required]
        public string OperatorName { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
