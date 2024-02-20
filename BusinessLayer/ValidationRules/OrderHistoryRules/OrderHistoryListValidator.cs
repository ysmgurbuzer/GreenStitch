using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.OrderHistoryRules
{
    public class OrderHistoryListValidator : AbstractValidator<OrderHistoryListDto>
    {
        public OrderHistoryListValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty();

        }
    }
}
