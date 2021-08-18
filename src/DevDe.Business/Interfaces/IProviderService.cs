using AppMvcBasic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevDe.Business.Interfaces
{
    public interface IProviderService
    {
        Task Add(Provider provider);
        Task Update(Provider provider);
        Task Remove(Guid id);
        
        Task UpdateAddress(Address address);

    }
}
