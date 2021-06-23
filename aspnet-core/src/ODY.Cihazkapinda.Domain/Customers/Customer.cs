using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.Customers
{
    public class Customer : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; protected set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NameAndSurname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public bool Active { get; set; }

        protected Customer()
        {

        }
        public Customer(Guid? tenantId,
            [NotNull] string _name,
            [NotNull] string _surname,
            [NotNull] string _phone,
            [NotNull] string _email,
            [NotNull] string _nameAndSurname,
            [NotNull] string _address,
            bool _active
            )
        {
            TenantId = tenantId;
            Name = Check.NotNullOrEmpty(_name, nameof(_name));
            Surname = Check.NotNullOrEmpty(_surname, nameof(_surname));
            Phone = Check.NotNullOrEmpty(_phone, nameof(_phone));
            EMail = Check.NotNullOrEmpty(_email, nameof(_email));
            NameAndSurname = Check.NotNullOrEmpty(_nameAndSurname, nameof(_nameAndSurname));
            Address = Check.NotNullOrEmpty(_address, nameof(_address));
            Active = _active;
        }
    }
}
