﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.ProductManagementModals
{
    public class ProductInfoTemplateEditModal : ProductInfoTemplateCreateModal
    {
        [HiddenInput]
        public Guid Id { get; set; }
    }
}
