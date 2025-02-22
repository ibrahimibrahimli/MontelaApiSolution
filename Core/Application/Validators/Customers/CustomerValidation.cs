using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            
        }
    }
}
