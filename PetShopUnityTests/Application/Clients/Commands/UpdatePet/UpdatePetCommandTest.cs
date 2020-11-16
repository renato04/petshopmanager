using FluentAssertions;
using Moq;
using PetShop.Domain.Application.Clients.Commands.UpdatePet;
using PetShop.Domain.Application.Mapping;
using PetShop.Domain.Models;
using PetShop.Domain.Models.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PetShopUnitTests.Application.Clients.Commands.UpdatePet
{
    public class UpdatePetCommandTest
    {
        [Fact]
        public async Task ShouldUpdate_A_Pet()
        {
            var name = "Vader";
            var request = new UpdatePetCommand(name, Guid.NewGuid());

            var mapper = PetShopMappingConfiguration.GetPetShopMappings();
            var mockRepository = new Mock<IPetRepository>();

            mockRepository.Setup(p => p.Update(It.Is<Pet>(c => c.Name == name)));

            var handler = new UpdatePetCommandHandler(mapper, mockRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.Message.Should().BeEquivalentTo("Pet Updated");
            mockRepository.Verify(m => m.Update(It.IsAny<Pet>()), Times.Once());
        }
    }
}
