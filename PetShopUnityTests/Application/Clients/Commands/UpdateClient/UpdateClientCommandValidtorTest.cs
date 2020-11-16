using FluentAssertions;
using PetShop.Domain.Application.Clients.Commands.UpdateClient;
using Xunit;

namespace PetShopUnitTests.Application.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandValidtorTest
    {
        [Fact]
        public void Shoud_have_error_when_name_is_empty()
        {
            var validator = new UpdateClientCommandValidator();

            var model = new UpdateClientCommand { Name = string.Empty };

            validator.Validate(model).IsValid.Should().BeFalse();
        }
    }
}
