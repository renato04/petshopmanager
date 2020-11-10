using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Models.Repository
{
    public interface IPetRepository
    {
        Task Add(Pet pet);
        Task Update(Pet pet);
        Task<Pet> GetById(Guid id);
        Task<IEnumerable<Pet>> GetAllByClient(Guid ClientId);
    }
}
