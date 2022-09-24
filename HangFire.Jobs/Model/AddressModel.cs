using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HangFire.Jobs.Models
{
    public class AddressModel
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}