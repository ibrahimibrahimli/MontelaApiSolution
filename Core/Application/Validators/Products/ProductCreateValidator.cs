using Application.Bases;
using Application.ViewModels.Products;
using FluentValidation;

namespace Application.Validators.Products
{
    public class ProductCreateValidator : AbstractValidator<ProductCreate>
    {
        public ProductCreateValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                     .WithMessage(BaseUiMessages.NOT_EMPTY_MESSAGE)
                .MaximumLength(150)
                     .WithMessage(BaseUiMessages.MAXIMUM_100_SYMBOL_MESSAGE)
                .MinimumLength(2)
                     .WithMessage(BaseUiMessages.MINIMUM_3_SYMBOL_MESSAGE);

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                     .WithMessage(BaseUiMessages.NOT_EMPTY_MESSAGE)
                .GreaterThanOrEqualTo(0)
                .WithMessage(BaseUiMessages.GREATER_THAN_0);
            //.LessThanOrEqualTo()

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                     .WithMessage(BaseUiMessages.NOT_EMPTY_MESSAGE)
                .GreaterThanOrEqualTo(0)
                .WithMessage(BaseUiMessages.GREATER_THAN_0);
        }
    }
}
