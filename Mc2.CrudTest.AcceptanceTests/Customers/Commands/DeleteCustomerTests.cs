using Application.Common.Exceptions;
using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.DeleteCustomer;
//using Application.TodoLists.Commands.CreateTodoList;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Mc2.CrudTest.AcceptanceTests.Customers.Commands
{

    using static Testing;

    public class DeleteCustomerTests : TestBase
    {
        [Test]
        public async Task ShouldRequireValidCustomerId()
        {
            var customer = new DeleteCustomerCommand { Id = 99 };

            await FluentActions.Invoking(() =>
                SendAsync(customer)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteCustomer()
        {
            var itemId = await SendAsync(new CreateCustomerCommand
            {
                Firstname = "Hamed",
                Lastname = "Lohi",
                Email = "lohi.hamed@gmail.com",
                DateOfBirth = DateTime.Parse("1990-02-25"),
                PhoneNumber = "+9809149012500",
                BankAccountNumber = "123456789"
            });

            await SendAsync(new DeleteCustomerCommand
            {
                Id = itemId
            });

            var item = await FindAsync<Customer>(itemId);

            item.Should().BeNull();
        }
    }
}