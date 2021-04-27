using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppMvcBasic.Models
{
    public class Product : Entity
    {
        public Guid ProviderId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Value { get; set; }

        public DateTime DateRegister { get; set; }

        public bool Active { get; set; }

        /*  EF Relation - One to One    */
        public Provider Provider { get; set; }
    }
}
