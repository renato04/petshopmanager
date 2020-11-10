using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Models.Repository
{
    public interface IClientRepository
    {
        Task Add(Client client);
        Task Update(Client client);
        Task<Client> GetById(Guid id);
        Task<IEnumerable<Client>> GetAll();
    }
}
