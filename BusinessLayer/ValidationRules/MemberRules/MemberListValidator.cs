using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.MemberRules
{
    public class MemberListValidator : AbstractValidator<MemberListDto>
    {
        public MemberListValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

        }
    }
}
