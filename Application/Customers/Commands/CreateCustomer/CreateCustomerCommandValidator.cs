using FluentValidation;

namespace Application.Customers.Commands.CreateCustomer
{

    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(v => v.Firstname)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}