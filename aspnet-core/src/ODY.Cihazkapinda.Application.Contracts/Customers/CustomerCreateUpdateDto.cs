using System;
using System.Collections.Generic;
using System.Text;

namespace ODY.Cihazkapinda.Customers
{
    public class CustomerCreateUpdateDto
    {
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NameAndSurname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public bool Active { get; set; }
    }
}
