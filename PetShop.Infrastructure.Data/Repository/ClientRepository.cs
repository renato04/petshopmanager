using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Models;
using PetShop.Domain.Models.Repository;
using PetShop.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Infrastructure.Data.Repository
{
    public class ClientRepository : IClientRepository
    {
        private PetShopDbContext _context;

        public ClientRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public async Task Add(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Client client)
        {
            _context.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.Clients.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Client> GetById(Guid id)
        {
            return await _context.Clients.Include(p => p.Pets).FirstOrDefaultAsync(p => p.Id == id);
        }


    }
}
