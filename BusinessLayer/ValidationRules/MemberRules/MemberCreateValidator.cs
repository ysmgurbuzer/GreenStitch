using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.MemberRules
{
    public class MemberCreateValidator : AbstractValidator<MemberCreateDto>
    {
        public MemberCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

        }
    }
}
