using FluentAssertions;
using PetShop.Domain.Application.Clients.Commands.AddPet;
using Xunit;

namespace PetShopUnitTests.Application.Clients.Commands.AddPet
{
    public class AddPetCommandValidatorTest
    {
        [Fact]
        public void Shoud_have_error_when_name_is_empty()
        {
            var validator = new AddPetCommandValidator();

            var model = new AddPetCommand { Name = string.Empty };

            validator.Validate(model).IsValid.Should().BeFalse();
        }
    }
}
