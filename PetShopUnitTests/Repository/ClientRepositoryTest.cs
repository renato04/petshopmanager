using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using PetShop.Domain.Models;
using PetShop.Infrastructure.Data.Context;
using PetShop.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PetShopUnitTests.Repository
{
    public class ClientRepositoryTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Should_Add_A_Clent()
        {
            var mockSet = new Mock<DbSet<Client>>();
            var mockContext = new Mock<PetShopDbContext>();
            mockContext.Setup(m => m.Clients).Returns(mockSet.Object);
            var repository = new ClientRepository(mockContext.Object);
            await repository.Add(new Client() { Name = "Lee" });
            mockSet.Verify(m => m.Add(It.IsAny<Client>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task GetAllClients_orders_by_name()
        {
            var data = new List<Client>
            {
                new Client { Name = "BBB" },
                new Client { Name = "ZZZ" },
                new Client { Name = "AAA" },
            }.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<PetShopDbContext>();
            mockContext.Setup(c => c.Clients).Returns(data.Object);

            var repository = new ClientRepository(mockContext.Object);
            var clients = await repository.GetAll();

            Assert.AreEqual(3, clients.Count());
            Assert.AreEqual("AAA", clients.ElementAt(0).Name);
            Assert.AreEqual("BBB", clients.ElementAt(1).Name);
            Assert.AreEqual("ZZZ", clients.ElementAt(2).Name);
        }

        [Test]
        public async Task GetClientById()
        {
            var id = new Guid("994aa42a-e292-42f1-b5d4-749cd19a4d29");

            var expected = new Client { Name = "BBB", Id = id, Pets = new List<Pet> { new Pet { Name = "Link" } } };

            var clients = new List<Client>
            {
                new Client { Name = "BBB", Id = id, Pets = new List<Pet>{ new Pet { Name = "Link" } } },
                new Client { Name = "ZZZ", Id = Guid.NewGuid()},
                new Client { Name = "AAA", Id = Guid.NewGuid()},
            };

            var mock = clients.AsQueryable().BuildMockDbSet();

            mock.Setup(p => p.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((object[] ids) =>
                {
                    var id = (Guid)ids.First();
                    return clients.FirstOrDefault(x => x.Id == id);
                });

            var mockContext = new Mock<PetShopDbContext>();
            mockContext.Setup(c => c.Clients).Returns(mock.Object);

            var repository = new ClientRepository(mockContext.Object);
            var client = await repository.GetById(id);

            client.Should().BeEquivalentTo(expected);
        }
    }
}