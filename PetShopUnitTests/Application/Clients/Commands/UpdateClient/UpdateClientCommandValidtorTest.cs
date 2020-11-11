using FluentAssertions;
using NUnit.Framework;
using PetShop.Domain.Application.Clients.Commands.UpdateClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopUnitTests.Application.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandValidtorTest
    {
        [Test]
        public void Shoud_have_error_when_name_is_empty()
        {
            var validator = new UpdateClientCommandValidator();

            var model = new UpdateClientCommand { Name = string.Empty };

            validator.Validate(model).IsValid.Should().BeFalse();
        }
    }
}
