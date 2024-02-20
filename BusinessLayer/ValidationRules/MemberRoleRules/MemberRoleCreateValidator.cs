using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.MemberRoleRules
{
    public class MemberRoleCreateValidator : AbstractValidator<MemberRoleCreateDto>
    {
        public MemberRoleCreateValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty();

        }
    }
}
