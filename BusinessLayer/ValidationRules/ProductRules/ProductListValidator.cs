using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.ProductRules
{
    public class ProductListValidator : AbstractValidator<ProductListDto>
    {
        public ProductListValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty();
        }
    }
}
