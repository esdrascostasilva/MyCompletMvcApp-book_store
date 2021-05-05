using AppMvcBasic.Models;
using DevDe.Business.Interfaces;
using DevDe.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevDe.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(MyDbContext context) : base(context) { }

        public async Task<Address> GetAddressByProvider(Guid providerId)
        {
            return await Db.Addresses.AsNoTracking()
                .FirstOrDefaultAsync(a => a.ProviderId == providerId);
        }
    }
}
