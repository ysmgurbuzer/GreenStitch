using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.FabricTypeRules
{
    public class FabricTypeCreateValidator:AbstractValidator<FabricTypeCreateDto>
    {
        public FabricTypeCreateValidator()
        {
            RuleFor(x => x.FabricName).NotEmpty();
            RuleFor(x => x.FabricDescription).NotEmpty();
        }
    }
}
