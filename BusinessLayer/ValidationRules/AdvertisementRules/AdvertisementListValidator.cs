using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.AdvertisementRules
{
    public class AdvertisementListValidator:AbstractValidator<AdvertisementListDto>
    {
        public AdvertisementListValidator()
        {
            RuleFor(x => x.Stock).NotEmpty();
        }
    }
}
