using FluentValidation;

namespace Application.Customers.Queries.GetCustomerById
{

    public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1).WithMessage("Id at least greater than or equal to 1.");
        }
    }
}