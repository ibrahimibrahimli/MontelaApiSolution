﻿using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class OrderValidation : AbstractValidator<Order>
    {
        public OrderValidation()
        {
            RuleFor(o => o.Address).NotEmpty()
                .NotNull();
        }
    }
}
