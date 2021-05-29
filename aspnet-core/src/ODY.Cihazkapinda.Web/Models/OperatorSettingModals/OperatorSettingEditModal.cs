using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.OperatorSettingModals
{
    public class OperatorSettingEditModal : OperatorSettingCreateModal
    {
        [HiddenInput]
        public Guid Id { get; set; }
    }
}
