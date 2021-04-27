using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppMvcBasic.Models
{
    public class Provider : Entity
    {
        public string Name { get; set; }

        public string Document { get; set; }

        public TypeProvider TypeProvider { get; set; }

        public Address Address { get; set; }

        public bool Active { get; set; }

        /* Lista de produtos declarada aqui para o EF entender que essa relação é de 1 para muitos com Produtos */
        /* EF Relations - One to Many */
        public IEnumerable<Product> Products { get; set; }
    }
}
