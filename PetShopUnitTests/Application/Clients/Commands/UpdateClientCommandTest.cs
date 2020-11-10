using FluentAssertions;
using Moq;
using NUnit.Framework;
using PetShop.Domain.Application.Clients.Commands;
using PetShop.Domain.Application.Clients.Dto;
using PetShop.Domain.Application.Mapping;
using PetShop.Domain.Models;
using PetShop.Domain.Models.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetShopUnitTests.Application.Clients.Commands
{
    public class UpdateClientCommandTest
    {
        [Test]
        public async Task ShouldUpdate_A_Client()
        {
            var name = "Vader";
            var request = new UpdateClientCommand { Name = name };

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
