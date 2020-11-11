using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Application.Clients.Commands.UpdatePet
{
    public class UpdatePetCommandValidator: AbstractValidator<UpdatePetCommand>
    {
        public UpdatePetCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name must have a value");
        }
    }
}
