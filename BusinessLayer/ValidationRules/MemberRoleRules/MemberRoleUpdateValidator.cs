using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.MemberRoleRules
{
    public class MemberRoleUpdateValidator : AbstractValidator<MemberRoleUpdateDto>
    {
        public MemberRoleUpdateValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty();

        }
    }
}
