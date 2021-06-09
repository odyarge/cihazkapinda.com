using JetBrains.Annotations;
using ODY.Cihazkapinda.Categories;
using ODY.Cihazkapinda.ProductColorTypes;
using ODY.Cihazkapinda.ProductTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.ProductManagement
{
    public class Product : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public int Installment { get; set; }
        public bool Discount { get; set; }
        public bool Active { get; set; }
        public ProductColorType ProductColor { get; set; }
        public ProductType ProductType { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductProperty> ProductProperty { get; set; }
        public ICollection<ProductInfo> ProductInfo { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }

        protected Product()
        {

        }
        public Product(Guid? tenantId,
            [NotNull] string _title,
            string _subTitle,
            [NotNull] string _description,
            decimal _price,
            decimal _discountPrice,
            int _installment,
            bool _discount,
            bool _active,
            ProductColorType _productColor,
            ProductType _productType,
            Guid _categoryId)
        {
            TenantId = tenantId;
            Title = Check.NotNullOrWhiteSpace(_title, nameof(_title));
            SubTitle = _subTitle;
            Description = Check.NotNullOrWhiteSpace(_description, nameof(_description));
            Price = _price;
            Installment = _installment;
            Discount = _discount;
            DiscountPrice = _discountPrice;
            Active = _active;
            ProductColor = _productColor;
            ProductType = _productType;
            CategoryId = _categoryId;
            Images = new Collection<ProductImage>();
            ProductProperty = new Collection<ProductProperty>();
            ProductInfo = new Collection<ProductInfo>();
        }
    }
}
