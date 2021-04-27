using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppMvcBasic.Models
{
    public class Address : Entity 
    {
        public Guid ProviderId { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        public string ZipCode { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        /*  EF Relation - One to One    */
        public Provider Provider { get; set; }
    }
}
