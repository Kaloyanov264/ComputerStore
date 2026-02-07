using ComputerStore.Models.Requests;
using FluentValidation;

namespace ComputerStore.Host.Validators
{
    public class AddCustomerRequestValidator : AbstractValidator<AddCustomerRequest>
    {
        public AddCustomerRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1).WithMessage("Name cannot be below 1 character.")
                .WithMessage("Name is required.");

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3).WithMessage("Email cannot be below 3 characters.")
                .WithMessage("Email is required.");

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative.")
                .LessThanOrEqualTo(1000).WithMessage("Discount cannot be above 1000.");
        }
    }
}
