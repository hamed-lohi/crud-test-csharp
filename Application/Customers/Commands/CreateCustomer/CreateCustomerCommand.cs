using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customers.Commands.CreateCustomer
{

    public class CreateCustomerCommand : IRequest<long>
    {
        
        [StringLength(100)]
        public string Firstname { get; set; }

        [StringLength(100)]
        public string Lastname { get; set; }

        //[StringLength(100)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Email is invalid!")]
        public string Email { get; set; }

        //[Phone]
        public string PhoneNumber { get; set; }

        [CreditCard(ErrorMessage = "Account Number is invalid!")]
        public string BankAccountNumber { get; set; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateCustomerCommand, long>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<long> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Customer(request.Firstname, request.Lastname, request.DateOfBirth, request.PhoneNumber, request.Email, request.BankAccountNumber);

            entity.DomainEvents.Add(new CustomerCreatedEvent(entity));

            _context.Customers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}