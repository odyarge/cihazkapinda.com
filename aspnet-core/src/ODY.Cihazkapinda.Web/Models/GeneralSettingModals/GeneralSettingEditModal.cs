using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.GeneralSettingModals
{
    public class GeneralSettingEditModal : GeneralSettingCreateModal
    {
        [HiddenInput]
        public Guid Id { get; set; }
    }
}
