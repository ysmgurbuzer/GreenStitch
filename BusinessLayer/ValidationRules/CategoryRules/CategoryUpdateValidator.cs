using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.CategoryRules
{
    public class CategoryUpdateValidator:AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty();
            RuleFor(x => x.CategoryName).MinimumLength(2);
        }
    }
}
