using Application.Bases;
using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(c => c.Name).NotEmpty()
                .NotNull()
                .WithMessage(BaseUiMessages.NOT_EMPTY_MESSAGE);

        }
    }
}
