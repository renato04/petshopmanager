using FluentAssertions;
using Moq;
using PetShop.Domain.Application.Clients.Commands.UpdateClient;
using PetShop.Domain.Application.Mapping;
using PetShop.Domain.Models;
using PetShop.Domain.Models.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PetShopUnitTests.Application.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandTest
    {
        [Fact]
        public async Task ShouldUpdate_A_Client()
        {
            var name = "Vader";
            var request = new UpdateClientCommand { Name = name, Id = Guid.NewGuid() };

            var mapper = PetShopMappingConfiguration.GetPetShopMappings();
            var mockRepository = new Mock<IClientRepository>();

            mockRepository.Setup(p => p.Update(It.Is<Client>(c => c.Name == name)));

            var handler = new UpdateClientCommandHandler(mapper, mockRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.Message.Should().BeEquivalentTo("Client Updated");
            mockRepository.Verify(m => m.Update(It.IsAny<Client>()), Times.Once());
        }
    }
}
