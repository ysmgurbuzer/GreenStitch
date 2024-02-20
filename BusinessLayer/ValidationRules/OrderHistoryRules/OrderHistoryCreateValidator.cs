using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.OrderHistoryRules
{
    public class OrderHistoryCreateValidator : AbstractValidator<OrderHistoryCreateDto>
    {
        public OrderHistoryCreateValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty();


        }
    }
}
