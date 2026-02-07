using ComputerStore.Models.Requests;
using FluentValidation;

namespace ComputerStore.Host.Validators
{
    public class AddComputerRequestValidator : AbstractValidator<AddComputerRequest>
    {
        public AddComputerRequestValidator()
        {
            RuleFor(x => x.Brand)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50).WithMessage("Brand cannot exceed 50 characters.")
                .MinimumLength(2).WithMessage("Brand cannot be below 2 characters.")
                .WithMessage("Brand is required.");

            RuleFor(x => x.Cpu)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50).WithMessage("Cpu cannot exceed 50 characters.")
                .MinimumLength(2).WithMessage("Cpu cannot be below 2 characters.")
                .WithMessage("Cpu is required.");

            RuleFor(x => x.Ram)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50).WithMessage("Ram cannot exceed 50 characters.")
                .MinimumLength(2).WithMessage("Ram cannot be below 2 characters.")
                .WithMessage("Ram is required.");

            RuleFor(x => x.Storage)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50).WithMessage("Storage cannot exceed 50 characters.")
                .MinimumLength(2).WithMessage("Storage cannot be below 2 characters.")
                .WithMessage("Storage is required.");

            RuleFor(x => x.Gpu)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50).WithMessage("Gpu cannot exceed 50 characters.")
                .MinimumLength(2).WithMessage("Gpu cannot be below 2 characters.")
                .WithMessage("Gpu is required.");

            RuleFor(x => x.Category)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20).WithMessage("Category cannot exceed 20 characters.")
                .MinimumLength(2).WithMessage("Category cannot be below 2 characters.")
                .WithMessage("Category is required.");

            RuleFor(x => x.BasePrice)
                .GreaterThan(0).WithMessage("Base price must be greater than 0.")
                .LessThan(1000000).WithMessage("Base price must be less than 1000000.")
                .WithMessage("Base price is required.");
        }
    }
}
