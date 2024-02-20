using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.NewFolder1
{
    public class ProductSizeUpdateValidator :AbstractValidator<ProductSizeUpdateDto>
    {
        public ProductSizeUpdateValidator()
        {
            RuleFor(x => x.Size).NotEmpty();
        }
    }
}
