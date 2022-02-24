using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customers.Commands.UpdateCustomer
{

    public class UpdateCustomerCommand : IRequest
    {
        public long Id { get; set; }

        //public string? Title { get; set; }

        //public bool Done { get; set; }

        [StringLength(100)]
        public string Firstname { get; set; }

        [StringLength(100)]
        public string Lastname { get; set; }

        //[StringLength(100)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is invalid!")]
        public string Email { get; set; }

        //[Phone]
        public string PhoneNumber { get; set; }

        [CreditCard(ErrorMessage = "Account Number is invalid!")]
        public string BankAccountNumber { get; set; }
    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Customer), request.Id);
            }

            entity.SetProperties(request.Firstname, request.Lastname, request.DateOfBirth, request.PhoneNumber, request.Email, request.BankAccountNumber);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}