using Application.Common.Exceptions;
using Application.Customers.Commands.CreateCustomer;
//using Application.TodoLists.Commands.CreateTodoList;
using Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using PhoneNumbers;
using System;
using System.Threading.Tasks;

namespace Mc2.CrudTest.AcceptanceTests.Customers.Commands
{

    using static Testing;

    public class CreateCustomerTests : TestBase
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            var customer = new CreateCustomerCommand();

            await FluentActions.Invoking(() =>
                SendAsync(customer)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateCustomer()
        {
            var customer = createCustomer();
            var itemId = await SendAsync(customer);

            var item = await FindAsync<Customer>(itemId);

            item.Should().NotBeNull();
            item.Firstname.Should().Be(customer.Firstname);
            item.CreatedBy.Should().BeNull();
            item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
            item.LastModifiedBy.Should().BeNull();
            item.LastModified.Should().BeNull();
        }

        [Test]
        public async Task ShouldReturnPhoneNumberParseException()
        {
            var customer = createCustomer();
            customer.PhoneNumber = "09149012500";// invalid phone number

            await FluentActions.Invoking(() =>
                SendAsync(customer)).Should().ThrowAsync<NumberParseException>();
        }

        [Test]
        public async Task ShouldThrowUniqueExceptionByFirstnameLastnameDateOfBirth()
        {
            var customer = createCustomer();
            var customer2 = createCustomer();
            customer2.Email = "hamed.lohi@yahoo.com";

            await SendAsync(customer);
            await FluentActions.Invoking(() =>
                SendAsync(customer2)).Should().ThrowAsync<DbUpdateException>();
        }

        [Test]
        public async Task ShouldThrowUniqueExceptionByEmail()
        {
            var customer = createCustomer();
            var customer2 = createCustomer();
            customer2.Firstname = "Ali";

            await SendAsync(customer);
            await FluentActions.Invoking(() =>
                SendAsync(customer2)).Should().ThrowAsync<DbUpdateException>();
        }

        private CreateCustomerCommand createCustomer()
        {
            CreateCustomerCommand customer = new CreateCustomerCommand()
            {
                Firstname = "Hamed",
                Lastname = "Lohi",
                Email = "lohi.hamed@gmail.com",
                DateOfBirth = DateTime.Parse("1990-02-25"),
                PhoneNumber = "+9809149012500",
                BankAccountNumber = "123456789"
            };

            return customer;
        }
        

    }
}