using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Domain.Models
{
    public class Pet : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Client Client { get; set; } = new Client();
        public Guid ClientId { get; set; }
    }
}
