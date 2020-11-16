using FluentAssertions;
using Moq;
using PetShop.Domain.Application.Clients.Commands.CreateClient;
using PetShop.Domain.Application.Mapping;
using PetShop.Domain.Models;
using PetShop.Domain.Models.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PetShopUnitTests.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommandTest
    {
        [Fact]
        public async Task ShouldCreate_A_Client()
        {
            string name = "Vader";
            Guid clientId = Guid.NewGuid();

            var request = new CreateClientCommand { Name = name };
            var expected = new CreateClientResponse { Name = name, Id = clientId };

            var mapper = PetShopMappingConfiguration.GetPetShopMappings();
            var mockRepository = new Mock<IClientRepository>();

            mockRepository.Setup(p => p.Add(It.Is<Client>(c => c.Name == name)))
                .Returns((Client client) => Task.Run(() =>
                {
                    client.Id = clientId;
                }));

            var handler = new CreateClientCommandHandler(mapper, mockRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.Data.Should().BeEquivalentTo(expected);
            result.Message.Should().BeEquivalentTo("Client Created");
            mockRepository.Verify(m => m.Add(It.IsAny<Client>()), Times.Once());
        }
    }
}
