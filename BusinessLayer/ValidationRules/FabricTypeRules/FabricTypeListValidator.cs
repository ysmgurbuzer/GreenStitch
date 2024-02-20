using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.FabricTypeRules
{
    public class FabricTypeListValidator:AbstractValidator<FabricTypeListDto>
    {
        public FabricTypeListValidator()
        {
            RuleFor(x => x.FabricName).NotEmpty();
            RuleFor(x => x.FabricDescription).NotEmpty();
        }
    }
}
