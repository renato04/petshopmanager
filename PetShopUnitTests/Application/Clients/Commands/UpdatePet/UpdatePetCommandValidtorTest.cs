using FluentAssertions;
using NUnit.Framework;
using PetShop.Domain.Application.Clients.Commands.UpdatePet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopUnitTests.Application.Clients.Commands.UpdatePet
{
    public class UpdatePetCommandValidtorTest
    {
        [Test]
        public void Shoud_have_error_when_name_is_empty()
        {
            var validator = new UpdatePetCommandValidator();

            var model = new UpdatePetCommand { Name = string.Empty };

            validator.Validate(model).IsValid.Should().BeFalse();
        }
    }
}
