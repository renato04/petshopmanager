﻿using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using PetShop.Domain.Models;
using PetShop.Infrastructure.Data.Context;
using PetShop.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PetShopUnitTests.Repository
{
    public class PetRepositoryTest
    {

        [Fact]
        public async Task Should_Add_A_Pet()
        {
            var mockSet = new Mock<DbSet<Pet>>();
            var mockContext = new Mock<PetShopDbContext>();
            mockContext.Setup(m => m.Pets).Returns(mockSet.Object);
            var repository = new PetRepository(mockContext.Object);
            await repository.Add(new Pet() { Name = "Lee" });
            mockSet.Verify(m => m.Add(It.IsAny<Pet>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task GetAllPets_by_client_orders_by_name()
        {
            var id = new Guid("994aa42a-e292-42f1-b5d4-749cd19a4d29");

            var data = new List<Pet>
            {
                new Pet { Name = "BBB", Client = new Client{Id = id } },
                new Pet { Name = "ZZZ", Client = new Client{Id = id } },
                new Pet { Name = "AAA", Client = new Client{Id = Guid.NewGuid()} },
            }.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<PetShopDbContext>();
            mockContext.Setup(c => c.Pets).Returns(data.Object);

            var repository = new PetRepository(mockContext.Object);
            var pets = await repository.GetAllByClient(id);

            pets.Count().Should().Be(2);
            pets.ElementAt(0).Name.Should().BeEquivalentTo("BBB");
            pets.ElementAt(1).Name.Should().BeEquivalentTo("ZZZ");
        }

        [Fact]
        public async Task GetPetById()
        {
            var id = new Guid("994aa42a-e292-42f1-b5d4-749cd19a4d29");

            var expected = new Pet { Name = "BBB", Id = id };

            var pets = new List<Pet>
            {
                new Pet{ Name = "BBB", Id = id},
                new Pet{ Name = "ZZZ", Id = Guid.NewGuid()},
                new Pet{ Name = "AAA", Id = Guid.NewGuid()},
            };

            var mock = pets.AsQueryable().BuildMockDbSet();

            mock.Setup(p => p.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((object[] ids) =>
                {
                    var id = (Guid)ids.First();
                    return pets.FirstOrDefault(x => x.Id == id);
                });

            var mockContext = new Mock<PetShopDbContext>();
            mockContext.Setup(c => c.Pets).Returns(mock.Object);

            var repository = new PetRepository(mockContext.Object);
            var pet = await repository.GetById(id);

            pet.Should().BeEquivalentTo(expected);
        }
    }
}
