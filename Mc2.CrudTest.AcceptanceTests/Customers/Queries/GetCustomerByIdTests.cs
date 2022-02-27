using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Queries.GetCustomerById;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mc2.CrudTest.AcceptanceTests.Customers.Queries
{

    using static Testing;

    public class GetCustomerByIdTests : TestBase
    {
        [Test]
        public async Task ShouldReturnNull()
        {
            var query = new GetCustomerByIdQuery() { Id = 100 };

            var result = await SendAsync(query);

            result.Should().BeNull();
        }

        [Test]
        public async Task ShouldReturnOne()
        {

            var customer = new CreateCustomerCommand()
            {
                Firstname = "Hamed",
                Lastname = "Lohi",
                Email = "lohi.hamed@gmail.com",
                DateOfBirth = DateTime.Parse("1990-02-25"),
                PhoneNumber = "+9809149012500",
                BankAccountNumber = "123456789"
            };

            var itemId = await SendAsync(customer);

            var query = new GetCustomerByIdQuery() { Id = itemId };

            var result = await SendAsync(query);

            result.Should().NotBeNull();
        }
    }
}