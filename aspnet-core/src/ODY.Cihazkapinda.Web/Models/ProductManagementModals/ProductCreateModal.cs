using Microsoft.AspNetCore.Mvc;
using ODY.Cihazkapinda.ProductColorTypes;
using ODY.Cihazkapinda.ProductTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ODY.Cihazkapinda.Web.Models.ProductManagementModals
{
    public class ProductCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "SubTitle")]
        public string SubTitle { get; set; }

        [Required]
        [Display(Name = "Description")]
        [TextArea(Rows = 4)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "DiscountPrice")]
        public decimal DiscountPrice { get; set; }

        [Display(Name = "Installment")]
        public int Installment { get; set; }

        [Display(Name = "Discount")]
        public bool Discount { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        [Display(Name = "ProductColor")]
        public ProductColorType ProductColor { get; set; } = ProductColorType.Yok;

        [Display(Name = "ProductType")]
        public ProductType ProductType { get; set; } = ProductType.Telefon;

        [Required]
        [HiddenInput]
        [Display(Name = "CategoryId")]
        public Guid CategoryId { get; set; }
    }
}
