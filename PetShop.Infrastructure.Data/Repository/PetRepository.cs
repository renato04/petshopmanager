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
    public class PetRepository : IPetRepository
    {
        private PetShopDbContext _context;

        public PetRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public async Task Add(Pet pet)
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Pet pet)
        {
            _context.Update(pet);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pet>> GetAllByClient(Guid ClientId) =>
            await _context.Pets
                .Where(p => p.Client.Id == ClientId)
                .OrderBy(p => p.Name)
                .ToListAsync();


        public async Task<Pet> GetById(Guid id) =>
            await _context.Pets.FindAsync(id);
    }
}
