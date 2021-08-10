using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PetShop.Domain.Models
{
    public class Client : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Pet> Pets { get; set; } = new List<Pet>();
    }
}
