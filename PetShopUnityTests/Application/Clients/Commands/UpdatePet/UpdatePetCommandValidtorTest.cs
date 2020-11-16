using FluentAssertions;
using PetShop.Domain.Application.Clients.Commands.UpdatePet;
using System;
using Xunit;

namespace PetShopUnitTests.Application.Clients.Commands.UpdatePet
{
    public class UpdatePetCommandValidtorTest
    {
        [Fact]
        public void Shoud_have_error_when_name_is_empty()
        {
            var validator = new UpdatePetCommandValidator();

            var model = new UpdatePetCommand(string.Empty, Guid.NewGuid());

            validator.Validate(model).IsValid.Should().BeFalse();
        }
    }
}
