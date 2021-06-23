using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Models.CustomerModals
{
    public class CustomerCreateModal
    {
        [HiddenInput]
        public Guid? TenantId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [HiddenInput]
        [Display(Name = "NameAndSurname")]
        public string NameAndSurname { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "EMail")]
        public string EMail { get; set; }
        [Display(Name = "Active")]
        [HiddenInput]
        public bool Active { get; set; }
    }
}
