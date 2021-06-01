using JetBrains.Annotations;
using ODY.Cihazkapinda.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.Categories
{
    public class Category : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string Name { get; set; }
        public Guid? SubCategory { get; set; }
        public bool Active { get; set; }
        public ICollection<Product> Products { get; set; }

        protected Category()
        {

        }
        public Category(Guid? tenantId,
            [NotNull] string _name,
            Guid? _subCategory,
            bool _active)
        {
            TenantId = tenantId;
            Name = Check.NotNullOrWhiteSpace(_name, nameof(_name));
            SubCategory = _subCategory;
            Active = _active;
        }
    }
}