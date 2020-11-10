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
    public class AddPetCommandTest
    {
        [Test]
        public async Task ShouldAdd_A_Pet()
        {
            string name = "Vader";

            var request = new AddPetCommand {  Name = name  };
            var expected = new AddPetResponse { Name = name, Id = new Guid("994aa42a-e292-42f1-b5d4-749cd19a4d29")};

            var mapper = PetShopMappingConfiguration.GetPetShopMappings();
            var mockRepository = new Mock<IPetRepository>();

            mockRepository.Setup(p => p.Add(It.Is<Pet>(c => c.Name == name)))
                .Returns((Pet pet) => Task.Run(() => { pet.Id = new Guid("994aa42a-e292-42f1-b5d4-749cd19a4d29"); }));

            var handler = new AddPetCommandHandler(mapper, mockRepository.Object);

            var result = await handler.Handle(request, CancellationToken.None);

            result.Data.Should().BeEquivalentTo(expected);
            result.Message.Should().BeEquivalentTo("Pet Added");
            mockRepository.Verify(m => m.Add(It.IsAny<Pet>()), Times.Once());
        }
    }
}
