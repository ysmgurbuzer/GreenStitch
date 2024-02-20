using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.RecyclingHistoryRules
{
    public class RecyclingHistoryListValidator : AbstractValidator<RecyclingHistoryListDto>
    {
        public RecyclingHistoryListValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty();
        }
    }
}

