using FluentAssertions;
using NUnit.Framework;
using PetShop.Domain.Application.Clients.Commands.AddPet;
using PetShop.Domain.Application.Clients.Commands.CreateClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopUnitTests.Application.Clients.Commands.AddPet
{
    public class AddPetCommandValidatorTest
    {
        [Test]
        public void Shoud_have_error_when_name_is_empty()
        {
            var validator = new AddPetCommandValidator();

            var model = new AddPetCommand { Name = string.Empty };

            validator.Validate(model).IsValid.Should().BeFalse();
        }
    }
}
