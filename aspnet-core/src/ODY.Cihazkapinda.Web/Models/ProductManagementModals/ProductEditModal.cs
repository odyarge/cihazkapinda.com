using Microsoft.AspNetCore.Mvc;
using ODY.Cihazkapinda.ProductManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.ProductManagementModals
{
    public class ProductEditModal : ProductCreateModal
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [NotMapped]
        public ICollection<ProductImageDto> Images { get; set; }

        [NotMapped]
        public ICollection<ProductPropertyDto> ProductProperty { get; set; }
        [NotMapped]
        public ICollection<ProductInfoDto> ProductInfo { get; set; }

    }
}
