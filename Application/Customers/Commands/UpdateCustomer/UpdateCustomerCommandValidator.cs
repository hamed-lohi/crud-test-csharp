using FluentValidation;

namespace Application.Customers.Commands.UpdateCustomer
{

    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(v => v.Firstname)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}