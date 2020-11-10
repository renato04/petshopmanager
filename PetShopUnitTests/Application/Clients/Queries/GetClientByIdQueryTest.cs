using FluentAssertions;
using Moq;
using NUnit.Framework;
using PetShop.Domain.Application.Clients.Dto;
using PetShop.Domain.Application.Clients.Queries;
using PetShop.Domain.Application.Mapping;
using PetShop.Domain.Models;
using PetShop.Domain.Models.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PetShopUnitTests.Application.Clients.Queries
{
    public class GetClientByIdQueryTest
    {
        [Test]
        public async Task Should_Return_a_Client()
        {
            var id = new Guid("994aa42a-e292-42f1-b5d4-749cd19a4d29");

            var expected = new ClientDto { Name = "BBB", Id = id, Pets = new List<PetDto> { new PetDto { Name = "Link" } } };

            var mapper = PetShopMappingConfiguration.GetPetShopMappings();
            var clientMock = new Mock<IClientRepository>();

            clientMock.Setup(p => p.GetById(id)).ReturnsAsync(new Client { Name = "BBB", Id = id, Pets = new List<Pet> { new Pet { Name = "Link" } } });

            var handler = new GetClientByIdQueryHandler(mapper, clientMock.Object);

            var result = await handler.Handle(new GetClientByIdQuery { Id = id }, CancellationToken.None);

            result.Data.Should().BeEquivalentTo(expected);
            clientMock.Verify(m => m.GetById(It.IsAny<Guid>()), Times.Once());
        }
    }
}
