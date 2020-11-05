using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Domain.Models.Repository
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll(int id);
    }
}
