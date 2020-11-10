using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace PetShop.Domain.Application.Clients.Dto
{
    public class ClientDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<PetDto> Pets { get; set; } = new List<PetDto>();
    }

    public class PetDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [IgnoreDataMember]
        public Guid ClientId { get; set; }
    }
}
