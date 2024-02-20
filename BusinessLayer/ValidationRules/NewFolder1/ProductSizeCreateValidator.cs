using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.NewFolder1
{
    public class ProductSizeCreateValidator : AbstractValidator<ProductSizeCreateDto>
    {
        public ProductSizeCreateValidator()
        {
            RuleFor(x => x.Size).NotEmpty();
        }
    }
    
}
