using FluentAssertions;
using Moq;
using NUnit.Framework;
using PetShop.Domain.Application.Clients.Commands;
using PetShop.Domain.Application.Clients.Commands.CreateClient;
using PetShop.Domain.Application.Clients.Dto;
using PetShop.Domain.Application.Mapping;
using PetShop.Domain.Models;
using PetShop.Domain.Models.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetShopUnitTests.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommandTest
    {
        [Test]
        public async Task ShouldCreate_A_Client()
        {
            string name = "Vader";

            var request = new CreateClientCommand {  Name = name };
            var expected = new CreateClientResponse { Name = name };

            var mapper = PetShopMappingConfiguration.GetPetShopMappings();
            var mockRepository = new Mock<IClientRepository>();

            mockRepository.Setup(p => p.Add(It.Is<Client>(c => c.Name == name)));

            var handler = new CreateClientCommandHandler(mapper, mockRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.Data.Should().BeEquivalentTo(expected);
            result.Message.Should().BeEquivalentTo("Client Created");
            mockRepository.Verify(m => m.Add(It.IsAny<Client>()), Times.Once());
        }
    }
}
