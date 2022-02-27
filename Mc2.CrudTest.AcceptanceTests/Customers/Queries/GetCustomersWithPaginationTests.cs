using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Queries.GetCustomersWithPagination;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mc2.CrudTest.AcceptanceTests.Customers.Queries
{

    using static Testing;

    public class GetCustomersWithPaginationTests : TestBase
    {
        [Test]
        public async Task ShouldReturnEmptyList()
        {
            var query = new GetCustomersWithPaginationQuery();

            var result = await SendAsync(query);

            result.Items.Should().BeEmpty();
        }

        [Test]
        public async Task ShouldReturnAllLists()
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

            var query = new GetCustomersWithPaginationQuery();

            var result = await SendAsync(query);

            result.Items.Should().HaveCount(1);
            //result.Items.First().Items.Should().HaveCount(7);
        }
    }
}