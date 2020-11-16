using FluentAssertions;
using PetShop.Domain.Application.Clients.Commands.CreateClient;
using Xunit;

namespace PetShopUnitTests.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommandValidatorTest
    {
        [Fact]
        public void Shoud_have_error_when_name_is_empty()
        {
            var validator = new CreateClientCommandValidator();

            var model = new CreateClientCommand { Name = string.Empty };

            validator.Validate(model).IsValid.Should().BeFalse();
        }
    }
}
