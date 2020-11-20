using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Api.Pet.Models
{
    public class RegisterResultModel
    {
        public bool Succesful { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}
