using Application.Common.Exceptions;
using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.UpdateCustomer;
//using Application.TodoLists.Commands.CreateTodoList;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Mc2.CrudTest.AcceptanceTests.Customers.Commands
{

    using static Testing;

    public class UpdateCustomerTests : TestBase
    {
        [Test]
        public async Task ShouldRequireValidCustomerId()
        {
            var customer = new UpdateCustomerCommand { Id = 99, Firstname = "Name" };
            await FluentActions.Invoking(() => SendAsync(customer)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTodoItem()
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

            var customer = new UpdateCustomerCommand
            {
                Id = itemId,
                Firstname = "Ali",
                Lastname = "Lohi",
                Email = "lohi.hamed@gmail.com",
                DateOfBirth = DateTime.Parse("1990-02-25"),
                PhoneNumber = "+9809149012500",
                BankAccountNumber = "123456789"
            };

            await SendAsync(customer);

            var item = await FindAsync<Customer>(itemId);

            item.Should().NotBeNull();
            item!.Firstname.Should().Be(customer.Firstname);
            //item.LastModifiedBy.Should().NotBeNull();
            item.LastModified.Should().NotBeNull();
            item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        }
    }
}