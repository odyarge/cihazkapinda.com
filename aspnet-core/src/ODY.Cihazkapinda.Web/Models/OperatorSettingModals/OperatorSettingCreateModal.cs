using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.OperatorSettingModals
{
    public class OperatorSettingCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }
        [Required]
        [Display(Name = "OperatorName")]
        public string OperatorName { get; set; }
        [Display(Name = "Image")]
        public string Image { get; set; }
    }
}
